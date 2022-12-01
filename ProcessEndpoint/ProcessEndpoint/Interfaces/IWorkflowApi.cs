using AutoWrapper.Server;
using ProcessEndpoint.Models;
using Refit;

namespace ProcessEndpoint.Interfaces
{
    public interface IWorkflowApi
    {
        [Post("/api/workflows/{id}/trigger")]
        Task<HttpResponseMessage> TriggerWorkflow([Header("Authorization")]string accessToken,string id, [Body] TriggerWorkflowDto model);

        [Get("/api/workflows")]
        Task<WrapTo<List<WorkflowDefinitionDto>>> GetWorkflows([Header("Authorization")] string accessToken, string? filters = "", string? sortOrder = "", int maxPageSize = 20 , int defaultPageSize = 10 , int pageNumber = 1);
    }
}