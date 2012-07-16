using System.Web.Mvc;

namespace Fabrik.Common.CMS
{
    public static class MarkupExtensions
    {
        public static MvcHtmlString RenderTrusted(this HtmlHelper html, object content, string format = ContentFormats.Markdown)
        {
            var renderer = DependencyResolver.Current.GetService<IContentRenderer>();
            var rendered = renderer.RenderTrusted((content ?? string.Empty).ToString(), format);
            return MvcHtmlString.Create(rendered);
        }

        public static MvcHtmlString RenderUntrusted(this HtmlHelper html, object content, string format = ContentFormats.Markdown)
        {
            var renderer = DependencyResolver.Current.GetService<IContentRenderer>();
            var rendered = renderer.RenderUntrusted((content ?? string.Empty).ToString(), format);
            return MvcHtmlString.Create(rendered);
        }
    }
}