using System.Web.Mvc;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Fabrik.Common.Web
{
    public class XmlViewResultFormatter : ViewResultFormatter
    {
        public XmlViewResultFormatter()
        {
            AddSupportedMediaType("text/xml");
        }

        public override ActionResult CreateResult(ControllerContext controllerContext)
        {
            var model = controllerContext.Controller.ViewData.Model;

            if (model == null)
                return null;

            var serializer = new XmlSerializer(model.GetType());

            var doc = new XDocument();

            using (var writer = doc.CreateWriter())
            {
                serializer.Serialize(writer, model);
                return new XmlResult(doc);
            }
        }
    }
}
