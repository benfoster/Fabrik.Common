using System.Web.Http;

namespace Fabrik.Common.WebAPI.AtomPubExample
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Handlers
            config.MessageHandlers.Add(new AtomPub.WLWMessageHandler());
            config.MessageHandlers.Add(new EnrichingHandler());

            // Formatters
            config.Formatters.Remove(config.Formatters.XmlFormatter);
            config.Formatters.Add(new AtomPub.AtomPubMediaFormatter());
            config.Formatters.Add(new AtomPub.AtomPubCategoryMediaTypeFormatter());

            // Filters
            config.Filters.Add(new ValidateModelStateAttribute());

            // Enrichers
            config.AddResponseEnrichers(new PostResponseEnricher());

            // Routes
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
