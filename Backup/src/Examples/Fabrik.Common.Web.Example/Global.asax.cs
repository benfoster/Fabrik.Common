using System.ComponentModel;
using System.Web.Mvc;
using System.Web.Routing;
using Fabrik.Common.Web.StructureMap;
using StructureMap;

namespace Fabrik.Common.Web.Example
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new InjectPageMetadataAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            BootStructureMap();
            
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            // use pluggable metadata provider - could do this automatically with an IoC container
            ModelMetadataProviders.Current = 
                new PluggableModelMetaDataProvider(new[] { new DescriptionAttributeMetadataPlugin() });

            // set up partial view formatter
            ViewResultFormatters.Formatters.Add(new PartialViewResultFormatter());
        }

        protected static void BootStructureMap()
        {
            ObjectFactory.Configure(cfg =>
            {
                cfg.For<IViewFactory>().Use<DefaultViewFactory>();

                cfg.Scan(scan =>
                {
                    scan.TheCallingAssembly();
                    scan.ConnectImplementationsToTypesClosing(typeof(IViewBuilder<>));
                    scan.ConnectImplementationsToTypesClosing(typeof(IViewBuilder<,>));
                });
            });
            
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(ObjectFactory.Container));
        }
    }

    public class DescriptionAttributeMetadataPlugin : AttributeMetadataPlugin<DescriptionAttribute>
    {
        public override void AssignMetadata(DescriptionAttribute attribute, ModelMetadata metadata)
        {
            metadata.Description = (attribute.Description ?? string.Empty).Trim();
        }
    }
}