using System.ServiceModel.Syndication;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An ActionResult for returning Atom feeds
    /// </summary>
    public class AtomResult : FeedResult
    {
        public AtomResult(SyndicationFeed feed) 
            : base(new Atom10FeedFormatter(feed), "application/atom+xml")
        {

        }
    }
}
