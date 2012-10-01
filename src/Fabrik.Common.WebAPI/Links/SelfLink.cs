
namespace Fabrik.Common.WebAPI.Links
{
    /// <summary>
    /// Conveys an identifier for the link's context.
    /// </summary>
    public class SelfLink : Link
    {
        public const string Relation = "self";

        public SelfLink(string href, string title = null) : base(Relation, href, title)
        {
        }
    }
}
