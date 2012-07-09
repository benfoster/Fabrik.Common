using System.Web.Mvc;
using System.Web.Routing;
using Machine.Fakes;
using Machine.Specifications;

namespace Fabrik.Common.Web.Specs
{
    public class AlertResultSpecs
    {
        [Subject(typeof(AlertResult<>))]
        public class When_executing_the_result : WithFakes
        {
            static ControllerContext controllerContext;
            static AlertResult<ContentResult> result;

            Establish ctx = () => {
                controllerContext = new ControllerContext(DynamicHttpContext.Create(), new RouteData(), An<ControllerBase>());
                var contentResult = new ContentResult { Content = "Test", ContentType = "text/plain" };

                result = contentResult.AndAlert(AlertType.Info, "Alert Title", "Alert Description");
            };

            Because of = ()
                => result.ExecuteResult(controllerContext);

            It Should_add_the_alert_to_tempdata = () => {
                var alert = controllerContext.Controller.TempData[typeof(Alert).FullName] as Alert;
                alert.ShouldNotBeNull();
                alert.Title.ShouldEqual("Alert Title");
                alert.Description.ShouldEqual("Alert Description");
            };

            It Should_return_the_original_result_without_modification = () => {
                controllerContext.HttpContext.Response.WasToldTo(x => x.Write("Test"));
                controllerContext.HttpContext.Response.ContentType.ShouldEqual("text/plain");
            };
        }
    }
}
