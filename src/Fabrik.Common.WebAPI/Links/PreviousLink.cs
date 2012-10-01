
namespace Fabrik.Common.WebAPI.Links
{
    /// <summary>
    /// Indicates that the link's context is a part of a series, and that the previous in the series is the link target.
    /// </summary>
    public class PreviousLink : Link
    {
        public const string Relation = "prev";

        public PreviousLink(string href, string title = null)
            : base(Relation, href, title)
        {
        }
    }
}
