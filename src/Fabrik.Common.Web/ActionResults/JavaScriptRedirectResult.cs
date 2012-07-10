using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An ActionResult for performing redirects via JavaScript
    /// </summary>
    public class JavaScriptRedirectResult : JavaScriptResult
    {
        private const string RedirectScriptFormat = "window.location = '{0}';";
        
        public JavaScriptRedirectResult(string redirectUrl)
        {
            Ensure.Argument.NotNullOrEmpty(redirectUrl, "redirectUrl");
            Script = RedirectScriptFormat.FormatWith(redirectUrl);
        }
    }
}
