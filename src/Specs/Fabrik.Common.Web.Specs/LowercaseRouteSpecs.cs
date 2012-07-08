using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;

namespace Fabrik.Common.Web.Specs
{
    [Subject(typeof(LowercaseRoute), "Generating outbound URLs")]
    public class When_the_route_values_contain_uppercase_characters
    {       
        static UrlHelper urlHelper;
        static string result;
        
        Establish ctx = () =>
        {
            RouteTable.Routes.Clear();
            RouteTable.Routes.MapLowerCaseRoute("Categories/{category}/Product/{id}", new { controller = "Products", action = "Details" });

            var requestContext = new RequestContext(DynamicHttpContext.Create(), new RouteData());
            urlHelper = new UrlHelper(requestContext, RouteTable.Routes);
        };

        Because of = () =>
            result = urlHelper.Action("Details", "Products", new { id = "1234", category = "Sport" });

        It Should_convert_them_to_lowercase = ()
            => result.ShouldEqual("/categories/sport/product/1234");
    }
}
