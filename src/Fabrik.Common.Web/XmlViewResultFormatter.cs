using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// A <see cref="MediaTypeViewResultFormatter"/> to format the model as XML.
    /// </summary>
    public class XmlViewResultFormatter : MediaTypeViewResultFormatter
    {
        public XmlViewResultFormatter()
        {
            AddSupportedMediaType("text/xml");
        }

        public override ActionResult CreateResult(ControllerContext controllerContext, ActionResult currentResult)
        {
            var model = controllerContext.Controller.ViewData.Model;

            if (model == null)
                return null;

            return new XmlResult(model);
        }
    }
}
