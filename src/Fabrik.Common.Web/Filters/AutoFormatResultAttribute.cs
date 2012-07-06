using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public class AutoFormatResultAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // ensure we're working with a view            
            if (filterContext.Result is ViewResult)
            {
                var formatter = ViewResultFormatters.GetFormatter(filterContext.HttpContext);
                if (formatter != null)
                {
                    filterContext.Result = formatter.CreateResult(filterContext);
                }
            }
            
            base.OnActionExecuted(filterContext);
        }
    }
}
