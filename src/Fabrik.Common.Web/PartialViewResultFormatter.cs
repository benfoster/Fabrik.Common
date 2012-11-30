using System;
using System.Linq;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public class PartialViewResultFormatter : IViewResultFormatter
    {
        private readonly string partialViewPrefix;

        public PartialViewResultFormatter(string partialViewPrefix = "_")
        {
            Ensure.Argument.NotNull(partialViewPrefix, "partialViewPrefix"); // can be empty
            this.partialViewPrefix = partialViewPrefix;
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
                return null;

            var viewName = viewResult.ViewName.NullIfEmpty() 
                ?? controllerContext.RequestContext.RouteData.GetRequiredString("action");

            if (viewName.IsNullOrEmpty())
                throw new InvalidOperationException("View name cannot be null.");

            var partialViewName = string.Concat(partialViewPrefix, viewName);

            // check if partial exists, otherwise we'll use the same view
            var partialExists = viewResult.ViewEngineCollection.FindPartialView(controllerContext, partialViewName).View != null;

            var partialViewResult = new PartialViewResult
            {
                ViewData = viewResult.ViewData,
                TempData = viewResult.TempData,
                ViewName = partialExists ? partialViewName : viewName,
                ViewEngineCollection = viewResult.ViewEngineCollection,
            };

            return partialViewResult;
        }
    }
}
