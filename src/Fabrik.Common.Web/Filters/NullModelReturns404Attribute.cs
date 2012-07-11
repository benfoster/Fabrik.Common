using System;
using System.Net;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An Action Filter that returns a 404 (Resource Not Found) if the <see cref="ViewResult.Model"/> is null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class NullModelReturns404Attribute : ActionFilterAttribute
    {
        public string Description { get; set; }
        public string ViewName { get; set; }

        public NullModelReturns404Attribute(string description = null)
        {
            Description = description;
            ViewName = "NotFound";
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var result = filterContext.Result as ViewResultBase;

            if (result != null && result.Model == null)
            {
                if (result is PartialViewResult)
                    filterContext.Result = new HttpStatusCodePartialViewResult(ViewName, HttpStatusCode.NotFound, Description);
                else
                    filterContext.Result = new HttpStatusCodeViewResult(ViewName, HttpStatusCode.NotFound, Description);
            }
            
            base.OnActionExecuted(filterContext);
        }
    }
}
