
namespace Fabrik.Common.WebAPI.Links
{
    /// <summary>
    /// An IRI that refers to the furthest preceding resource in a series of resources.
    /// </summary>
    public class FirstLink : Link
    {
        public const string Relation = "first";

        public FirstLink(string href, string title = null)
            : base(Relation, href, title)
        {
        }
    }
}
