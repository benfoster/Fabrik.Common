using System.Web.Mvc;
using System;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An Action Filter that attempts to format the result using a <see cref="ViewResultFormatter"/>.
    /// </summary>
    /// <remarks>
    /// To add additional formatters use the <see cref="ViewResultFormatters.Formatters.Add(IViewResultFormatter)"/> method.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple=false, Inherited = true)]
    public class AutoFormatResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var formatter = ViewResultFormatters.GetFormatter(filterContext.HttpContext);
            if (formatter != null)
            {
                filterContext.Result = formatter.CreateResult(filterContext);
            }
            
            base.OnActionExecuted(filterContext);
        }
    }
}
