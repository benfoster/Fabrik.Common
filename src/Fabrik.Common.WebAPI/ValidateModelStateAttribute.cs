using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Fabrik.Common.WebAPI
{
    /// <summary>
    /// An <see cref="ActionFilterAttribute"/> for validating Model State.
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest, actionContext.ModelState);
            }
        }
    }
}
