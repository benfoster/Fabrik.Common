using System.Web.Mvc;
using System.Web.Routing;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Mvc;

namespace Fabrik.Common.Web.Tests
{  
    [Subject(typeof(ValidateModelStateAttribute))]
    public class ValidateModelStateAttributeTests : WithFakes
    {
        static ControllerBase controller;
        static ValidateModelStateAttribute attribute;
        static ActionExecutingContext actionExecutingContext;

        private class TestController : Controller
        {
        }

        Establish ctx = () =>
        {
            actionExecutingContext = An<ActionExecutingContext>();
            var routeData = new RouteData();
            routeData.Values.Add("action", "index");
            actionExecutingContext.RouteData = routeData;
            controller = new TestController { ViewData = new ViewDataDictionary(), TempData = new TempDataDictionary() };
            actionExecutingContext.Controller = controller;

            attribute = new ValidateModelStateAttribute();
        };

        public class When_model_state_is_valid
        {
            Because of = () =>
                attribute.OnActionExecuting(actionExecutingContext);

            It Should_not_set_the_result = () =>
                actionExecutingContext.Result.ShouldBeNull(); // action should be executed normally
        }

        public class When_model_state_is_invalid
        {
            Establish ctx = ()
                => controller.ViewData.ModelState.AddModelError("test", "error"); // invalidate modelstate
            
            Because of = () =>
                attribute.OnActionExecuting(actionExecutingContext);

            It Should_copy_modelstate_to_temp_data = () => {
                // get modelstate from tempdata
                var modelState = controller.TempData[typeof(ModelStateTempDataTransfer).FullName] as ModelStateDictionary;
                modelState["test"].Errors[0].ErrorMessage.ShouldEqual("error");
            };

            It Should_perform_a_redirect_to_the_same_action = () =>
                actionExecutingContext.Result.ShouldBeARedirectToRoute().And().ActionName().ShouldEqual("index");
        }

        public class When_model_state_is_invalid_and_AJAX_request
        {
            Establish ctx = () => {
                // simlulate ajax request
                actionExecutingContext.HttpContext.Request.WhenToldTo(x => x["X-Requested-With"]).Return("XMLHttpRequest");
                // invalidate modelstate
                controller.ViewData.ModelState.AddModelError("test", "error");
            };
            
            Because of = () => 
                attribute.OnActionExecuting(actionExecutingContext);

            It Should_return_a_400_error = () => 
                ((HttpStatusCodeResult)actionExecutingContext.Result).StatusCode.ShouldEqual(400);

            It Should_serialize_the_modelstate_errors_as_json = () => 
                ((HttpStatusCodeResult)actionExecutingContext.Result).StatusDescription.ShouldEqual(@"{""test"":""error""}");
        }
    }  
}
