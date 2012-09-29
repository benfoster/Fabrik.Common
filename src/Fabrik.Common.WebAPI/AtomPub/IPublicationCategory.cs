
namespace Fabrik.Common.WebAPI.AtomPub
{
    public interface IPublicationCategory
    {
        /// <summary>
        /// Required. The name (term) of the category.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Optional.
        /// </summary>
        string Label { get; }
    }
}
