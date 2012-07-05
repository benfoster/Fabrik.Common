using System.Xml.Linq;

namespace Fabrik.Common
{
    /// <summary>
    /// Extension methods for classes in the <see cref="System.Xml"/> namespace.
    /// </summary>
    public static class XmlExtensions
    {
        /// <summary>
        /// Sets the default xml namespace of every element in the given xml element
        /// </summary>
        public static void SetDefaultXmlNamespace(this XElement xelem, XNamespace xmlns)
        {
            Ensure.Argument.NotNull(xelem, "xelem");
            Ensure.Argument.NotNull(xmlns, "xmlns");
            
            if (xelem.Name.NamespaceName == string.Empty)
                xelem.Name = xmlns + xelem.Name.LocalName;
            
            foreach (var e in xelem.Elements())
                e.SetDefaultXmlNamespace(xmlns);
        }
    }
}
