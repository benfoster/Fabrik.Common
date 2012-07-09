using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap.Configuration.DSL;

namespace Fabrik.Common.Web.StructureMap
{
    /// <summary>
    /// A StructureMap registry for hooking up ASP.NET dependencies
    /// </summary>
    public class AspNetRegistry : Registry
    {
        public AspNetRegistry()
        {
            For<RouteCollection>().Use(RouteTable.Routes);
            For<GlobalFilterCollection>().Use(GlobalFilters.Filters);
            For<HttpContextBase>().Use(ctx => new HttpContextWrapper(HttpContext.Current));
            For<HttpServerUtilityBase>().Use(ctx => new HttpServerUtilityWrapper(HttpContext.Current.Server));
        }
    }
}
