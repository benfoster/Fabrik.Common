using MarkdownSharp;

namespace Fabrik.Common.CMS {
    public class MarkdownFormatter : IContentFormatter {
        public string Format(string content) {
            var md = new Markdown(new MarkdownOptions { AutoHyperlink = true, LinkEmails = true });
            return md.Transform(content);
        }
    }
}