using System.Collections.Generic;
using System.Xml.Linq;

namespace Fabrik.Common.Web
{
    public interface ISitemapGenerator
    {
        XDocument GenerateSiteMap(IEnumerable<ISitemapItem> items);
    }
}
