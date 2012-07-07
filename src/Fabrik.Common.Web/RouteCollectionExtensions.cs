using System.Web.Mvc;
using System.Web.Routing;

namespace Fabrik.Common.Web
{
    public static class RouteCollectionExtensions
    {
        public static Route MapLowerCaseRoute(this RouteCollection routes, string url, object defaults) {
            return routes.MapLowerCaseRoute(url, defaults, null);
        }

        public static Route MapLowerCaseRoute(this RouteCollection routes, string url, object defaults, object constraints) {
            
            Route route = new LowercaseRoute(url, new MvcRouteHandler()) {
                Defaults = new RouteValueDictionary(defaults),
                Constraints = new RouteValueDictionary(constraints)
            };

            routes.Add(null, route);

            return route;
        }
    }
}
