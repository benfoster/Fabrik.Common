using Fabrik.Common;
using System.Runtime.Serialization;

namespace Fabrik.Common.WebAPI.Links
{
    /// <summary>
    /// A base class for relation links
    /// </summary>
    [DataContract]
    public class Link
    {
        [DataMember]
        public string Rel { get; private set; }
        
        [DataMember]
        public string Href { get; private set; }
        
        [DataMember]
        public string Title { get; private set; }

        public Link(string rel, string href, string title = null)
        {
            Ensure.Argument.NotNullOrEmpty(rel, "rel");
            Ensure.Argument.NotNullOrEmpty(href, "href");

            Rel = rel;
            Href = href;
            Title = title;
        }

        public override string ToString()
        {
            return Href;
        }
    }
}
