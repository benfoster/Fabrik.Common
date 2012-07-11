using System.Web;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public interface IViewResultFormatter
    {
        bool IsSatisfiedBy(ControllerContext controllerContext);
        ActionResult CreateResult(ControllerContext controllerContext, ActionResult currentResult);
    }
}
