using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fabrik.Common.WebAPI.AtomPub
{
    /// <summary>
    /// An interface for commands that can create or update publications.
    /// </summary>
    public interface IPublicationCommand
    {
        /// <summary>
        /// The title of the publication.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// A short description of the content.
        /// </summary>
        string Summary { get; set; }

        /// <summary>
        /// The publication content.
        /// </summary>
        string Content { get; set; }

        /// <summary>
        /// The publication content type e.g. 'text' or 'html'.
        /// </summary>
        string ContentType { get; set; }

        /// <summary>
        /// An optional publish date for the entry.
        /// </summary>
        DateTime? PublishDate { get; set; }

        /// <summary>
        /// A string array of categories related to the content.
        /// </summary>
        string[] Categories { get; set; }
    }
}
