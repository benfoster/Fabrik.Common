using System;
using System.Net;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An ActionFilter for automatically validating ModelState before a controller action is executed.
    /// Performs a Redirect if ModelState is invalid. Assumes the <see cref="ImportModelStateFromTempDataAttribute"/> is used on the GET action.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ValidateModelStateAttribute : ModelStateTempDataTransfer
    {
        public ValidateModelStateAttribute() { }
        
        public ValidateModelStateAttribute(string actionName)
        {
            this.actionName = actionName;
        }
        
        public ValidateModelStateAttribute(string actionName, string controllerName)
        {
            this.actionName = actionName;
            this.controllerName = controllerName;
        }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.Controller.ViewData.ModelState.IsValid)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    ProcessAjax(filterContext);
                }
                else
                {
                    ProcessNormal(filterContext);
                }
            }
            
            base.OnActionExecuting(filterContext);
        }

        protected virtual void ProcessNormal(ActionExecutingContext filterContext)
        {
            // Export ModelState to TempData so it's available on next request
            ExportModelStateToTempData(filterContext);

            var routeValues = new RouteValueDictionary(filterContext.RouteData.Values);

            if (!string.IsNullOrWhiteSpace(this.actionName))
            {
                routeValues["action"] = this.actionName;
            }

            if (!string.IsNullOrWhiteSpace(this.controllerName))
            {
                routeValues["controller"] = this.controllerName;
            }

            // redirect back to GET action
            filterContext.Result = new RedirectToRouteResult(routeValues);
        }

        protected virtual void ProcessAjax(ActionExecutingContext filterContext)
        {
            var errors = filterContext.Controller.ViewData.ModelState.ToSerializableDictionary();
            var json = new JavaScriptSerializer().Serialize(errors);

            // send 400 status code (Bad Request)
            filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.BadRequest, json);
        }
    }
}
