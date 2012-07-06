using System.Web;
using System.Web.Mvc;

namespace Fabrik.Common.Web
{
    public interface IViewResultFormatter
    {
        bool IsSatisfiedBy(HttpContextBase httpContext);
        ActionResult CreateResult(ControllerContext controllerContext);
    }
}
