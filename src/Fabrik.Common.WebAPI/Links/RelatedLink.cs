
namespace Fabrik.Common.WebAPI.Links
{
    /// <summary>
    /// Identifies a related resource.
    /// </summary>
    public class RelatedLink : Link
    {
        public const string Relation = "related";

        public RelatedLink(string href, string title = null)
            : base(Relation, href, title)
        {
        }
    }
}
