using Elsa.ActivityResults;
using Elsa.Attributes;
using Elsa.Providers.WorkflowStorage;
using Elsa.Services;
using Elsa.Services.Models;
using ElsaEdiBackend.Activities.CreateFile;
using ElsaEdiBackend.Domain.WorkflowDefinitions.Dtos;
using ElsaEdiBackend.Services;
using Newtonsoft.Json;

namespace ElsaEdiBackend.Activities
{
    [Trigger(
        DisplayName = "Create File",
        Category = "EDI",
        Description = "This activity will create file with input coming from an http request"
    )]
    public class CreateFileActivity : Activity
    {
        private readonly IFileStorage _fileStorage;
        private readonly ILogger<CreateFileActivity> _logger;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateFileActivity(ILogger<CreateFileActivity> logger, IFileStorage fileStorage, IWebHostEnvironment hostingEnvironment)
        {
            _logger = logger;
            _fileStorage = fileStorage;
            _hostingEnvironment = hostingEnvironment;
        }

        #region Output Attributes

        [ActivityOutput(
            Hint = "The path of the file.",
            DefaultWorkflowStorageProvider = TransientWorkflowStorageProvider.ProviderName, DisableWorkflowProviderSelection = true)]
        public string Output { get; set; } = default!;

        #endregion Output Attributes

        protected override async ValueTask<IActivityExecutionResult> OnExecuteAsync(ActivityExecutionContext context)
        {
            var input = context.GetInput<TriggerWorkflowDto>();
            if (input == null || input?.Payload == null)
                return Fault("Input of CreateFile Activity is empty");

            var dto = JsonConvert.DeserializeObject<CreateFileActivityPayload>(input.Payload);

            if (dto == null)
                return Fault("Input of CreateFile Activity is empty");

            // Ensure Folder creation if FolderPath exist
            var contentRootPath = _hostingEnvironment.ContentRootPath;
            if (!string.IsNullOrEmpty(dto.FolderPath))
            {
                var path = Path.Combine(contentRootPath, dto.FolderPath);
                Directory.CreateDirectory(path);
            }
            var filePath = dto.GetFilePath();
            await _fileStorage.WriteAsync(dto.GetBytes(), filePath, context.CancellationToken); ;

            Output = filePath;

            return Done();
        }
    }
};