using Microsoft.AspNetCore.Mvc;
using ProcessEndpoint.Models;

namespace ProcessEndpoint.Controllers
{
    public class BaseController : Controller
    {       
        /// <summary>
        /// Sets the information for the system notification.
        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void Message(string message, string title, NotificationType notifyType)
        {
            TempData["NotificationMessage"] = message;
            TempData["NotificationTitle"] = title;
            TempData["NotificationType"] = notifyType;           
        }
    }
}