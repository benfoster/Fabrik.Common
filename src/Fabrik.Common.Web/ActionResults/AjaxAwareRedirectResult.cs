using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An AJAX aware version of <see cref="System.Web.Mvc.RedirectResult"/>
    /// </summary>
    public class AjaxAwareRedirectResult : RedirectResult
    {       
        public AjaxAwareRedirectResult(string url, bool permanent = false) : base(url, permanent)
        {
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context.HttpContext.Request.IsAjaxRequest())
            {
                var redirectUrl = UrlHelper.GenerateContentUrl(Url, context.HttpContext);
                var javaScriptRedirectResult = new JavaScriptRedirectResult(redirectUrl);

                javaScriptRedirectResult.ExecuteResult(context);
            }
            else
            {
                base.ExecuteResult(context);
            }
        }
    }
}
