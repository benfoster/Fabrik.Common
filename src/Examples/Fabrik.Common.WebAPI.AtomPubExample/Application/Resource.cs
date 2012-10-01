using Fabrik.Common.WebAPI.Links;
using System.Collections.Generic;

namespace Fabrik.Common.WebAPI.AtomPubExample
{
    public abstract class Resource
    {
        private readonly List<Link> links;

        public IEnumerable<Link> Links { get { return links; } }

        public Resource()
        {
            links = new List<Link>();
        }

        public void AddLink(Link link)
        {
            Ensure.Argument.NotNull(link, "link");
            links.Add(link);
        }
    }
}