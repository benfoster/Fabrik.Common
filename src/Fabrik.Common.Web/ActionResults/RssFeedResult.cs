using System.ServiceModel.Syndication;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An ActionResult for returning RSS feeds.
    /// </summary>
    public class RssFeedResult : FeedResult
    {
        public RssFeedResult(SyndicationFeed feed)
            : base(new Rss20FeedFormatter(feed), "application/rss+xml")
        {
            
        }
    }
}
