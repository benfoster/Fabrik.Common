using System.Web.Mvc;
using System.Web.Routing;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An AJAX aware version of <see cref="System.Web.Mvc.RedirectToRouteResult"/>
    /// </summary>
    public class AjaxAwareRedirectToRouteResult : ActionResult
    {
        private readonly RedirectToRouteResult redirectResult;

        public AjaxAwareRedirectToRouteResult(RedirectToRouteResult redirectResult)
        {
            Ensure.Argument.NotNull(redirectResult, "redirectResult");
            this.redirectResult = redirectResult;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                var redirectUrl = UrlHelper.GenerateUrl(
                    redirectResult.RouteName, 
                    null /* actionName */, 
                    null /* controllerName */, 
                    redirectResult.RouteValues, 
                    RouteTable.Routes, // RedirectToRouteResult.Routes is unaccessible
                    context.RequestContext, 
                    false /* includeImplicitMvcValues */);
                
                var javaScriptRedirectResult = new JavaScriptRedirectResult(redirectUrl);
                javaScriptRedirectResult.ExecuteResult(context);
            }
            else
            {
                redirectResult.ExecuteResult(context);
            }
        }
    }
}
