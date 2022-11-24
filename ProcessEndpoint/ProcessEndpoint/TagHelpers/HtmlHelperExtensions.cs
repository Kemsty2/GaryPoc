using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProcessEndpoint.TagHelpers;

public static class HtmlHelperExtensions
{
    public static HtmlString? ActiveSpanString(this IHtmlHelper htmlHelper, string controller, string action)
    {
        var routeData = htmlHelper.ViewContext.RouteData;
        var routeAction = routeData?.Values?["action"]?.ToString();
        var routeController = routeData?.Values?["controller"]?.ToString();

        var returnActive = (controller == routeController && action == routeAction);

        return returnActive ? new HtmlString(@"<span
                          class='absolute inset-y-0 left-0 w-1 bg-green-500 rounded-tr-lg rounded-br-lg'
                          aria-hidden='true'
                        ></span>") : null;
    }
}