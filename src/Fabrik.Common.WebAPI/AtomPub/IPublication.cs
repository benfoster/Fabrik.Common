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
        string Id { get; }
        string Title { get; }
        string Summary { get; }
        string Content { get; }
        string ContentType { get; }
        DateTime LastUpdated { get; }
        DateTime? PublishDate { get; }
        IEnumerable<string> Categories { get; }
        IEnumerable<Link> Links { get; }
    }
}
