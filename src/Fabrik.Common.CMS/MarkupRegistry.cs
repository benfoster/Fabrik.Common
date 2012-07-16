using System.Collections.Generic;
using StructureMap.Configuration.DSL;

namespace Fabrik.Common.CMS {
    public class MarkupRegistry : Registry {
        public MarkupRegistry() {
            For<IContentRenderer>().Use<ContentRenderer>();
            For<IContentFormatter>().Add<MarkdownFormatter>().Named(ContentFormats.Markdown);
            For<IContentFormatter>().Add<HtmlFormatter>().Named(ContentFormats.HTML);

            For<IDictionary<string, IContentFormatter>>().Use(ctx => {
                var dictionary = new Dictionary<string, IContentFormatter>();
                dictionary.Add(ContentFormats.Markdown, ctx.GetInstance<IContentFormatter>(ContentFormats.Markdown));
                dictionary.Add(ContentFormats.HTML, ctx.GetInstance<IContentFormatter>(ContentFormats.HTML));
                return dictionary;
            });
        }
    }
}
