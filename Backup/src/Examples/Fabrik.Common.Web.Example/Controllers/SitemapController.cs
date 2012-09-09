using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Fabrik.Common.Web.Example.Controllers
{
    public class SitemapController : Controller
    {
        //
        // GET: /Sitemap/

        public ActionResult Index()
        {
            var sitemapItems = new List<SitemapItem>
            {
                new SitemapItem(Url.QualifiedAction("index", "home"), changeFrequency: SitemapChangeFrequency.Always, priority: 1.0),
                new SitemapItem(Url.QualifiedAction("about", "home"), lastModified: DateTime.Now),
                new SitemapItem(PathUtils.CombinePaths(Request.Url.GetLeftPart(UriPartial.Authority), "/home/list"))
            };

            return new SitemapResult(sitemapItems);
        }

    }
}
