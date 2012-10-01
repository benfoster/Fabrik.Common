using Fabrik.Common.WebAPI.AtomPub;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fabrik.Common.WebAPI.AtomPubExample.Controllers
{
    public class TagsController : BlogControllerBase
    {
        // GET api/tags
        public PublicationCategoriesDocument Get()
        {
            var tags = posts.SelectMany(p => p.Tags)
                .Distinct(StringComparer.InvariantCultureIgnoreCase)
                .Select(t => new TagModel { Name = t, Slug = t.ToSlug() });

            var doc = new PublicationCategoriesDocument(
                Url.Link("DefaultApi", new { controller = "tags" }),
                tags,
                isFixed: false
            );

            return doc;
        }
    }
}
