using Fabrik.Common.WebAPI.Links;
using System;
using System.Collections.Generic;

namespace Fabrik.Common.WebAPI.AtomPub
{
    /// <summary>
    /// An interface for publications that can be returned as Atom entries.
    /// </summary>
    public interface IPublication
    {
        /// <summary>
        /// A permanent, universally unique identifier. Must be a valid IRI, as defined by [RFC3987].
        /// </summary>
        string Id { get; }

        /// <summary>
        /// A human-readable title for the publication.
        /// </summary>
        string Title { get; }

        /// <summary>
        /// A short summary, abstract, or excerpt of a publication.
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// The content of the publication.
        /// </summary>
        string Content { get; }

        /// <summary>
        /// The type of content. Either "text", "html", "xhtml" or a valid MIME media type.
        /// </summary>
        string ContentType { get; }
        
        /// <summary>
        /// The most recent instant in time when the publication was modified.
        /// </summary>
        DateTime LastUpdated { get; }

        /// <summary>
        /// The initial creation or first availability of the publication.
        /// </summary>
        DateTime? PublishDate { get; }

        /// <summary>
        /// A collection of categories associated with the publication.
        /// </summary>
        IEnumerable<IPublicationCategory> Categories { get; }

        /// <summary>
        /// An IRI categorization scheme for all the categories associated with the publication.
        /// </summary>
        string CategoriesScheme { get; }

        /// <summary>
        /// A collection of related resource links.
        /// </summary>
        IEnumerable<Link> Links { get; }
    }
}
