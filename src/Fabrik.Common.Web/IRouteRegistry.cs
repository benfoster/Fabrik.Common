using System.Web.Routing;

namespace Fabrik.Common.Web
{
    public interface IRouteRegistry
    {
        void RegisterRoutes(RouteCollection routes);
    }
}
