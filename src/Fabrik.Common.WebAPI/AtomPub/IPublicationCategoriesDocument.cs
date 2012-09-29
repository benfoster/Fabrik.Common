using System.Collections.Generic;

namespace Fabrik.Common.WebAPI.AtomPub
{
    public interface IPublicationCategoriesDocument
    {
        string Scheme { get; }
        bool IsFixed { get; }
        IEnumerable<IPublicationCategory> Categories { get; }
    }
}
