using System.Web.Mvc;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// Exposes the MVC HtmlHelper to Razor helpers instead of the crappy System.Web.WebPages HtmlHelper
    /// </summary>
    public class HelperPage : System.Web.WebPages.HelperPage
    {
        public static new HtmlHelper Html
        {
            get { return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Html; }
        }

        public static new UrlHelper Url
        {
            get { return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).Url; }
        }

        public static ViewDataDictionary ViewData
        {
            get { return ((System.Web.Mvc.WebViewPage)WebPageContext.Current.Page).ViewData; }
        }
    }
}
