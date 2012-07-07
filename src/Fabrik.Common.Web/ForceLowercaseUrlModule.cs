using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Net;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// A HTTP Module for forcing lower case urls. Issues a 301 Redirect for any URLs containing uppercase characters.
    /// </summary>
    /// <remarks>
    /// Taken from the FunnelWeb project http://www.funnelweblog.com/
    /// </remarks>
    public class ForceLowercaseUrlModule : HttpModuleBase
    {
        private static readonly string[] extensions = new[] { ".js", ".css", ".jpg", ".jpeg", ".gif", ".ico", ".png" };

        public override void OnBeginRequest(HttpContextBase context)
        {
            if (context.Request.Url.AbsolutePath.StartsWith("get", StringComparison.InvariantCultureIgnoreCase)
                    || context.Request.Url.AbsolutePath.StartsWith("/get", StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            if (context.Request.Url.AbsolutePath.EndsWith(".axd", StringComparison.InvariantCultureIgnoreCase)
                || context.Request.HttpMethod != "GET"
                || extensions.Any(x => context.Request.Url.AbsolutePath.EndsWith(x, StringComparison.InvariantCultureIgnoreCase)))
            {
                return;
            }

            var idealUrl = context.Request.Url.GetLeftPart(UriPartial.Path).ToLower(CultureInfo.InvariantCulture);

            if (idealUrl.EndsWith("/") && context.Request.Url.AbsolutePath.LastIndexOf('/') > 0)
            {
                idealUrl = idealUrl.Substring(0, idealUrl.LastIndexOf('/'));
            }

            if (!string.IsNullOrEmpty(context.Request.Url.Query))
            {
                idealUrl += context.Request.Url.Query;
            }

            if (context.Request.Url.AbsoluteUri == idealUrl || context.Request.Url.AbsoluteUri == idealUrl + "/")
                return;

            context.Response.Clear();
            context.Response.StatusCode = (int)HttpStatusCode.MovedPermanently;
            context.Response.Status = "301 Moved Permanently";
            context.Response.RedirectLocation = idealUrl;
            context.Response.End();
        }
    }
}
