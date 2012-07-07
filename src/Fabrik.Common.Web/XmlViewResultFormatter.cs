using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// A <see cref="ViewResultFormatter"/> to format the model as XML.
    /// </summary>
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

            return new XmlResult(model);
        }
    }
}
