using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An AJAX aware version of <see cref="System.Web.Mvc.RedirectResult"/>
    /// </summary>
    public class AjaxAwareRedirectResult : ActionResult
    {
        private readonly RedirectResult redirectResult;
        
        public AjaxAwareRedirectResult(RedirectResult redirectResult)
        {
            Ensure.Argument.NotNull(redirectResult, "redirectResult");
            this.redirectResult = redirectResult;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                var redirectUrl = UrlHelper.GenerateContentUrl(redirectResult.Url, context.HttpContext);
                var javaScriptRedirectResult = new JavaScriptRedirectResult(redirectUrl);

                javaScriptRedirectResult.ExecuteResult(context);
            }
            else
            {
                redirectResult.ExecuteResult(context);
            }
        }
    }
}
