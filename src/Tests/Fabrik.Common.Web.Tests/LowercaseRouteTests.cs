using System.Web.Mvc;
using System.Web.Routing;
using Machine.Specifications;

namespace Fabrik.Common.Web.Tests
{
    [Subject(typeof(LowercaseRoute))]
    public class When_generating_an_outbound_url
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

        It Should_be_in_lowercase = ()
            => result.ShouldEqual("/categories/sport/product/1234");
    }
}
