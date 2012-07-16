using System.Web.Mvc;

namespace Fabrik.Common.CMS {
    public interface IContentEnricher {
        string Enrich(string content, bool isContentTrusted);
    }
}