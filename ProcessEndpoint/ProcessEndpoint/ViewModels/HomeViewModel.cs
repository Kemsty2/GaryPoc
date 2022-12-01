using AutoWrapper.Server;
using ProcessEndpoint.Models;

namespace ProcessEndpoint.ViewModels
{
    public class HomeViewModel
    {
        public List<WorkflowDefinitionDto> Workflows { get; set; }

        public HomeViewModel()
        {
            Workflows = new List<WorkflowDefinitionDto>();
        }

        public HomeViewModel(WrapTo<List<WorkflowDefinitionDto>> workflows)
        {
            Workflows = workflows.Result;
        }
    }
}
