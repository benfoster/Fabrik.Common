using System.Web.Mvc;
using System.Web.Routing;
using Machine.Fakes;
using Machine.Specifications;
using Machine.Specifications.Mvc;

namespace Fabrik.Common.Web.Specs
{  
    [Subject(typeof(ValidateModelStateAttribute), "Validating ModelState")]
    public class ValidateModelStateAttributeSpecs : WithFakes
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

        public class When_the_modelstate_is_valid
        {
            Because of = () =>
                attribute.OnActionExecuting(actionExecutingContext);

            It Should_not_set_the_result = () =>
                actionExecutingContext.Result.ShouldBeNull(); // action should be executed normally
        }

        public class When_the_modelstate_is_invalid
        {
            Establish ctx = ()
                => controller.ViewData.ModelState.AddModelError("test", "error"); // invalidate modelstate
            
            Because of = () =>
                attribute.OnActionExecuting(actionExecutingContext);

            It Should_copy_the_modelstate_to_tempdata = () => {
                // get modelstate from tempdata
                var modelState = controller.TempData[typeof(ModelStateTempDataTransfer).FullName] as ModelStateDictionary;
                modelState["test"].Errors[0].ErrorMessage.ShouldEqual("error");
            };

            It Should_perform_a_redirect_to_the_same_action = () =>
                actionExecutingContext.Result.ShouldBeARedirectToRoute().And().ActionName().ShouldEqual("index");
        }

        public class When_the_modelstate_is_invalid_and_it_is_an_AJAX_request
        {
            Establish ctx = () => {
                // simlulate ajax request
                actionExecutingContext.HttpContext.Request.WhenToldTo(x => x["X-Requested-With"]).Return("XMLHttpRequest");
                // invalidate modelstate
                controller.ViewData.ModelState.AddModelError("test", "error");
            };
            
            Because of = () => 
                attribute.OnActionExecuting(actionExecutingContext);

            It Should_return_a_400_bad_request_error = () => 
                ((HttpStatusCodeResult)actionExecutingContext.Result).StatusCode.ShouldEqual(400);

            It Should_serialize_the_modelstate_errors_into_JSON = () => 
                ((HttpStatusCodeResult)actionExecutingContext.Result).StatusDescription.ShouldEqual(@"{""test"":""error""}");
        }
    }  
}
