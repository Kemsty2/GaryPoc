using ProcessEndpoint.Models;
using Refit;

namespace ProcessEndpoint.Interfaces
{
    public interface IWorkflowApi
    {
        [Post("/file-saver")]
        Task<HttpResponseMessage> TriggerWorkflow([Header("Authorization")]string accessToken, [Body] FileSaveViewModel model);
    }
}