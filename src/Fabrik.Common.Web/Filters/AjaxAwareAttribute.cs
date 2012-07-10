using System;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An Action Filter that automatically changes the result if it is an AJAX request.
    /// </summary>
    /// <remarks>
    /// Returns AJAX aware versions of <see cref="ViewResult"/>, <see cref="RedirectResult"/> and <see cref="RedirectToRouteResult"/>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AjaxAwareAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ConvertResultIf<ViewResult>(filterContext, x => new AjaxAndChildActionAwareViewResult(x));
            ConvertResultIf<RedirectResult>(filterContext, x => new AjaxAwareRedirectResult(x));
            ConvertResultIf<RedirectToRouteResult>(filterContext, x => new AjaxAwareRedirectToRouteResult(x));

            base.OnActionExecuted(filterContext);
        }

        protected static void ConvertResultIf<TResult>(ActionExecutedContext filterContext, Func<TResult, ActionResult> converter) where TResult : ActionResult
        {
            if (filterContext.Result is TResult)
            {               
                filterContext.Result = converter(filterContext.Result as TResult);
            }
        }
    }
}
