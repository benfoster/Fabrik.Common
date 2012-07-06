using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public class JsonViewResultFormatter : ViewResultFormatter
    {
        public JsonViewResultFormatter()
        {
            AddSupportedMediaType("application/json");
        }
        
        public override ActionResult CreateResult(ControllerContext controllerContext)
        {
            var model = controllerContext.Controller.ViewData.Model;

            if (model == null)
                return null;

            return new JsonResult { Data = model, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
