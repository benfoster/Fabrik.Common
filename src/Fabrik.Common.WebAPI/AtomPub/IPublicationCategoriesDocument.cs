using System.Collections.Generic;

namespace Fabrik.Common.WebAPI.AtomPub
{
    /// <summary>
    /// An interface for generating AtomPub Category Documents.
    /// </summary>
    public interface IPublicationCategoriesDocument
    {
        /// <summary>
        /// An IRI categorization scheme for all the categories contained within this document.
        /// </summary>
        string Scheme { get; }

        /// <summary>
        /// Indicates whether the list of categories is a fixed or an open set.
        /// </summary>
        bool IsFixed { get; }

        /// <summary>
        /// The categories within this document.
        /// </summary>
        IEnumerable<IPublicationCategory> Categories { get; }
    }
}
