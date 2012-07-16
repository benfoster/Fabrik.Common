using System.Web.Mvc;

namespace Fabrik.Common.CMS {
    /// <summary>
    /// Renders content (trusted or untrusted) by resolving a formatter that matches the given content format (e.g., HTML vs. Markdown).
    /// </summary>
    public interface IContentRenderer {
        string RenderTrusted(string content, string format);
        string RenderUntrusted(string content, string format);
    }
}