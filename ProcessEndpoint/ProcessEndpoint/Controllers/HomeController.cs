using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProcessEndpoint.Interfaces;
using ProcessEndpoint.Models;
using ProcessEndpoint.ViewModels;
using System.Diagnostics;

namespace ProcessEndpoint.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IWorkflowApi _workflowApi;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWorkflowApi workflowApi)
        {
            _logger = logger;
            _workflowApi = workflowApi;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var accessToken = await GetAccessToken();
            if (accessToken == null)
                return RedirectToAction("Logout");

            var workflows = await _workflowApi.GetWorkflows(accessToken);
            return View(new HomeViewModel(workflows));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> TriggerWorkflow([Bind("WorkflowId,Payload")] TriggerWorkflowViewModel model)
        {
            _logger.LogInformation("Trigger Workflow with Payload: {0}", model.ToString());
            var accessToken = await GetAccessToken();

            if (accessToken == null)
                return RedirectToAction("Logout");

            try
            {
                var response = await _workflowApi.TriggerWorkflow(accessToken, model.WorkflowId, new TriggerWorkflowDto(model.Payload));
                if (response.IsSuccessStatusCode)
                {
                    Message("Congrutulations! You've successfully trigger your workflow.", "Workflow Have been triggerred successfully", NotificationType.Success);
                }
                else
                {
                    Message("An error occurred when triggering the workflow", "An error ocurred", NotificationType.Error);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An unknown error occurred when triggering workflow");
                Message("An error occurred when triggering the workflow", "An error ocurred", NotificationType.Error);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            return SignOut(CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string?> GetAccessToken()
        {
            var token = await HttpContext.GetTokenAsync("access_token");
            if (token == null)
                return null;
            return $"Bearer {token}";
        }
    }
}