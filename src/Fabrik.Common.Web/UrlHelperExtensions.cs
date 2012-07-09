using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Returns a full qualified action URL
        /// </summary>
        public static string QualifiedAction(this UrlHelper url, string actionName, string controllerName, object routeValues = null)
        {
            return url.Action(actionName, controllerName, routeValues, url.RequestContext.HttpContext.Request.Url.Scheme);
        }
    }
}
