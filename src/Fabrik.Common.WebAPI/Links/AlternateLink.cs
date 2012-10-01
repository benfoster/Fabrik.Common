
namespace Fabrik.Common.WebAPI.Links
{
    /// <summary>
    /// Refers to a substitute for this context
    /// </summary>
    public class AlternateLink : Link
    {
        public const string Relation = "alternate";

        public AlternateLink(string href, string title = null)
            : base(Relation, href, title)
        {
        }
    }
}
