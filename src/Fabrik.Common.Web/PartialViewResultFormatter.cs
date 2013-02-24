using System;
using System.Linq;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public class PartialViewResultFormatter : IViewResultFormatter
    {
        private readonly string partialViewPrefix;
        private readonly string viewOverrideParameter;

        public PartialViewResultFormatter(string partialViewPrefix = "_", string viewOverrideParameter = null)
        {
            Ensure.Argument.NotNull(partialViewPrefix, "partialViewPrefix");
            this.partialViewPrefix = partialViewPrefix;
            this.viewOverrideParameter = viewOverrideParameter;
        }
        
        public virtual bool IsSatisfiedBy(ControllerContext controllerContext)
        {
            return controllerContext.HttpContext.Request.AcceptTypes.Contains("text/html")
                && (controllerContext.HttpContext.Request.IsAjaxRequest() || controllerContext.IsChildAction);
        }

        public virtual ActionResult CreateResult(ControllerContext controllerContext, ActionResult currentResult)
        {
            var viewResult = currentResult as ViewResult;

            if (viewResult == null)
            {
                return null;
            }
   
            var partialViewResult = new PartialViewResult
            {
                ViewData = viewResult.ViewData,
                TempData = viewResult.TempData,
                ViewName = GetPartialViewName(viewResult, controllerContext),
                ViewEngineCollection = viewResult.ViewEngineCollection,
            };

            return partialViewResult;
        }

        protected string GetPartialViewName(ViewResult viewResult, ControllerContext controllerContext)
        {           
            var routeData = controllerContext.RequestContext.RouteData;
            var viewName = viewResult.ViewName.NullIfEmpty() ?? routeData.GetRequiredString("action");

            // Check for view name override (child actions only)
            if (viewOverrideParameter.IsNotNullOrEmpty() && controllerContext.IsChildAction)
            {
                var overrideView = routeData.Values.GetOrDefault(viewOverrideParameter) as string;
                if (overrideView.IsNotNullOrEmpty())
                {
                    return overrideView;
                }
            }

            // Otherwise use partial view prefix
            
            if (viewName.IsNullOrEmpty())
            {
                throw new InvalidOperationException("View name cannot be null.");
            }
                    
            var partialViewName = string.Concat(partialViewPrefix, viewName);
            // check if partial exists, otherwise we'll use the same view but with no layout page
            var partialExists = viewResult.ViewEngineCollection.FindPartialView(controllerContext, partialViewName).View != null;

            return partialExists ? partialViewName : viewName;
        }
    }
}
