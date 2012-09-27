
namespace Fabrik.Common.WebAPI.Links
{
    /// <summary>
    /// An IRI that refers to the furthest following resource in a series of resources.
    /// </summary>
    public class LastLink : Link
    {
        public const string Relation = "last";

        public LastLink(string href, string title = null)
            : base(Relation, href, title)
        {
        }
    }
}
