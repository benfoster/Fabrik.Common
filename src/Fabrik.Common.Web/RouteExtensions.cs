using System.Web.Routing;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// Extensions for <see cref="System.Web.Routing.Route"/>.
    /// </summary>
    public static class RouteExtensions
    {
        /// <summary>
        /// Provides a fluent way of adding constraints to a route.
        /// </summary>
        public static Route WithConstraint(this Route route, string key, IRouteConstraint contraint)
        {
            route.Constraints.Add(key, contraint);
            return route;
        }
    }
}
