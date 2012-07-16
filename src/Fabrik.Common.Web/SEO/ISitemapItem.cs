using System;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// An interface for sitemap items
    /// </summary>
    public interface ISitemapItem
    {
        /// <summary>
        /// URL of the page.
        /// </summary>
        string Url { get; }

        /// <summary>
        /// The date of last modification of the file.
        /// </summary>
        DateTime? LastModified { get; }

        /// <summary>
        /// How frequently the page is likely to change.
        /// </summary>
        SitemapChangeFrequency? ChangeFrequency { get; }
        
        /// <summary>
        /// The priority of this URL relative to other URLs on your site. Valid values range from 0.0 to 1.0.
        /// </summary>
        double? Priority { get; }
    }
}
