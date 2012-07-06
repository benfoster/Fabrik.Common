using System;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace Fabrik.Common.Web
{
    public class XmlResult : ActionResult
    {
        public XDocument Xml { get; protected set; }
        public string ContentType { get; set; }
        public Encoding Encoding { get; set; }

        public XmlResult(XDocument xml)
        {
            if (xml == null)
                throw new ArgumentNullException("xml");

            Xml = xml;
            ContentType = "text/xml";
            Encoding = Encoding.UTF8;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;

            response.ContentType = ContentType;
            response.HeaderEncoding = Encoding;

            using (var writer = new XmlTextWriter(response.OutputStream, Encoding.UTF8))
            {
                Xml.WriteTo(writer);
            }
        }
    }
}
