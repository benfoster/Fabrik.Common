using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public static class ActionResultExtensions
    {
        /// <summary>
        /// Wraps the <typeparamref name="TResult"/> in an <see cref="AlertResult{TResult}"/> that inserts the alert message into TempData.
        /// Not compatible with <see cref="AjaxAwareAttribute"/> since it masks the underlying result.
        /// </summary>
        public static AlertResult<TResult> AndAlert<TResult>(this TResult result, AlertType alertType, string title, string description = null) where TResult : ActionResult
        {
            Ensure.Argument.NotNullOrEmpty(title, "title");
            return new AlertResult<TResult>(result, alertType, title, description);
        }

        /// <summary>
        /// Inserts the alert message into TempData before returning the <typeparamref name="TResult"/>.
        /// This is compatible with <see cref="AjaxAwareAttribute"/>.
        /// </summary>
        public static TResult AndAlert<TResult>(this TResult result, ControllerBase controller, AlertType alertType, string title, string description = null) where TResult : ActionResult
        {
            Ensure.Argument.NotNullOrEmpty(title, "title");
            var alert = new Alert(alertType, title, description);
            controller.TempData[typeof(Alert).FullName] = alert;
            return result;
        }
    }
}
