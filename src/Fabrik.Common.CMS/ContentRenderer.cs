using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fabrik.Common.CMS
{
    /// <summary>
    /// Renders content by looking up an appropriate <see cref="IContentFormatter"/> that matches the given format name (e.g., HTML or Markdown).
    /// </summary>
    public class ContentRenderer : IContentRenderer {
        private readonly IDictionary<string, IContentFormatter> formatters;
        private readonly IEnumerable<IContentEnricher> enrichers;

        public ContentRenderer(IDictionary<string, IContentFormatter> formatters, IEnumerable<IContentEnricher> enrichers) {
            this.formatters = formatters;
            this.enrichers = enrichers;
        }

        public string RenderTrusted(string content, string format) {
            return Render(content, format, true);
        }

        public string RenderUntrusted(string content, string format) {
            return Render(content, format, false);
        }

        private string Render(string content, string format, bool trusted) {
            IContentFormatter formatter;
            if (!formatters.TryGetValue(format, out formatter)) {
                throw new Exception("Unable to render content of format '{0}'. No provider is registered.");
            }

            content = formatter.Format(content);

            foreach (var enricher in enrichers) {
                content = enricher.Enrich(content, trusted);
            }

            return content;
        }
    }
}