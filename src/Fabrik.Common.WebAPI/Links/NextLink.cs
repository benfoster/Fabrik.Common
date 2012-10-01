
namespace Fabrik.Common.WebAPI.Links
{
    /// <summary>
    /// Indicates that the link's context is a part of a series, and that the next in the series is the link target.
    /// </summary>
    public class NextLink : Link
    {
        public const string Relation = "next";

        public NextLink(string href, string title = null)
            : base(Relation, href, title)
        {
        }
    }
}
