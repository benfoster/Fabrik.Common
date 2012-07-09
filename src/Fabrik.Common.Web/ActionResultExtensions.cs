using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public static class ActionResultExtensions
    {
        public static AlertResult<TResult> AndAlert<TResult>(this TResult result, AlertType flashType, string title, string description = null) where TResult : ActionResult
        {
            Ensure.Argument.NotNullOrEmpty(title, "title");
            return new AlertResult<TResult>(result, flashType, title, description);
        }
    }
}
