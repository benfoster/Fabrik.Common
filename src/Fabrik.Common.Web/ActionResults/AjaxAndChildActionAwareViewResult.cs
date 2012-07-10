using System;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An ActionResult that returns the <paramref name="viewResult"/> as a Partial view if the request
    /// is an AJAX request or Child Action.
    /// </summary>
    public class AjaxAndChildActionAwareViewResult : ActionResult
    {
        private readonly ViewResult viewResult;
        private readonly string partialViewPrefix;

        public AjaxAndChildActionAwareViewResult(ViewResult viewResult, string partialViewPrefix = "_")
        {
            Ensure.Argument.NotNull(viewResult, "viewResult");
            Ensure.Argument.NotNull(partialViewPrefix, "partialViewPrefix"); 
            this.viewResult = viewResult;
            this.partialViewPrefix = partialViewPrefix;
        }
        
        public override void ExecuteResult(ControllerContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest() || context.Controller.ControllerContext.IsChildAction)
            {
                var viewName = viewResult.ViewName.NullIfEmpty() ?? context.RequestContext.RouteData.GetRequiredString("action");

                if (viewName.IsNullOrEmpty())
                    throw new InvalidOperationException("View name cannot be null.");

                var partialViewName = string.Concat(partialViewPrefix, viewName);

                // check if partial exists, otherwise we'll use the same view
                var partialExists = viewResult.ViewEngineCollection.FindPartialView(context, partialViewName).View != null;

                var partialViewResult = new PartialViewResult
                {
                    ViewData = viewResult.ViewData,
                    TempData = viewResult.TempData,
                    ViewName = partialExists ? partialViewName : viewName,
                    ViewEngineCollection = viewResult.ViewEngineCollection,
                };

                partialViewResult.ExecuteResult(context);
            }
            else 
            {
                viewResult.ExecuteResult(context);
            }           
        }
    }
}
