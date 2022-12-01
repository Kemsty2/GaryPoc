using Elsa.Persistence.EntityFramework.Core.Extensions;
using Elsa.Providers.WorkflowStorage;
using ElsaEdiBackend.Activities;
using ElsaEdiBackend.Bookmarks;
using ElsaEdiBackend.Databases;
using Microsoft.EntityFrameworkCore;

namespace ElsaEdiBackend.Extensions.Services
{
    public static class ElsaExtensions
    {
        public static void AddElsaServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Elsa");

            services
                 .AddElsa(elsa => elsa
                     .UseEntityFrameworkPersistence<ElsaCustomContext>(ef => ef.UseSqlite(connectionString), false)
                     .AddConsoleActivities()
                     .UseDefaultWorkflowStorageProvider<TransientWorkflowStorageProvider>()
                     .AddActivitiesFrom<CreateFileActivity>())
                 .AddElsaApiEndpoints();

            services.WithJavaScriptOptions(options => options.EnableConfigurationAccess = true);

            services.AddBookmarkProvidersFrom<CreateFileBookmarkProvider>();
        }
    }
}