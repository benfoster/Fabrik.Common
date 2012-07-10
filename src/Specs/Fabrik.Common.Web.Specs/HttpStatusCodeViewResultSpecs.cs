using System.Net;
using System.Web.Mvc;
using System.Web.Routing;
using Machine.Fakes;
using Machine.Specifications;

namespace Fabrik.Common.Web.Specs
{
    public class HttpStatusCodeViewResultSpecs 
    {
        [Subject(typeof(HttpStatusCodeViewResult), "Executing the result")]
        public class Executing_the_result : WithFakes
        {            
            static ControllerContext controllerContext;
            static HttpStatusCodeViewResult result;

            public class When_it_is_a_normal_request
            {
                Establish ctx = () =>
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "test");

                    controllerContext = new ControllerContext(DynamicHttpContext.Create(), routeData, An<ControllerBase>());
                    result = new HttpStatusCodeViewResult("NotFound", HttpStatusCode.NotFound, "Contact not found.");

                    // need to set the view to avoid MVC looking for one
                    result.View = An<IView>();
                };


                Because of = ()
                    => result.ExecuteResult(controllerContext);

                It Should_set_the_response_status_code = ()
                    => controllerContext.HttpContext.Response.StatusCode.ShouldEqual((int)HttpStatusCode.NotFound);

                It Should_set_the_response_status_description = ()
                    => controllerContext.HttpContext.Response.StatusDescription.ShouldEqual("Contact not found.");

                It Should_render_a_view = ()
                    => result.View.WasToldTo(x => x.Render(Param.IsAny<ViewContext>(), Param.IsAny<System.IO.TextWriter>()));
            }

            public class When_it_is_an_AJAX_request
            {
                Establish ctx = () =>
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "test");

                    var httpContext = DynamicHttpContext.Create();
                    httpContext.SetupAjaxRequest();

                    controllerContext = new ControllerContext(httpContext, routeData, An<ControllerBase>());
                    result = new HttpStatusCodeViewResult("NotFound", HttpStatusCode.NotFound, "Contact not found.");
                    result.View = An<IView>();
                };

                Because of = ()
                    => result.ExecuteResult(controllerContext);

                It Should_set_the_response_status_code = ()
                    => controllerContext.HttpContext.Response.StatusCode.ShouldEqual((int)HttpStatusCode.NotFound);

                It Should_set_the_response_status_description = ()
                    => controllerContext.HttpContext.Response.StatusDescription.ShouldEqual("Contact not found.");

                It Should_not_render_a_view = ()
                    => result.View.WasNotToldTo(x => x.Render(Param.IsAny<ViewContext>(), Param.IsAny<System.IO.TextWriter>()));
            }
        }
    }
}
