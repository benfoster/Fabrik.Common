using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public class JsonViewResultFormatter : MediaTypeViewResultFormatter
    {
        public JsonViewResultFormatter()
        {
            AddSupportedMediaType("application/json");
        }
        
        public override ActionResult CreateResult(ControllerContext controllerContext, ActionResult currentResult)
        {
            var model = controllerContext.Controller.ViewData.Model;

            if (model == null)
                return null;

            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
