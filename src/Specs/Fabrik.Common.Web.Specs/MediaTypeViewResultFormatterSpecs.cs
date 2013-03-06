using Machine.Fakes;
using Machine.Specifications;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Fabrik.Common.Web.Specs
{
    [Subject(typeof(MediaTypeViewResultFormatter))]
    public class MediaTypeViewResultFormatterSpecs
    {
        static MediaTypeViewResultFormatter formatter;
        static ControllerContext controllerContext;
        static bool result;
        
        public class When_the_request_accept_header_is_empty : WithFakes
        {
            Establish ctx = () =>
            {
                formatter = new TestFormatter();
                var httpContext = DynamicHttpContext.Create();               
                controllerContext = new ControllerContext(DynamicHttpContext.Create(), new RouteData(), An<ControllerBase>());
            };

            Because of = () => result = formatter.IsSatisfiedBy(controllerContext);

            It Should_not_be_satisfied = () => result.ShouldBeFalse();
        }

        public class TestFormatter : MediaTypeViewResultFormatter
        {
            public TestFormatter()
            {
                AddSupportedMediaType("application/test");
            }
            
            public override ActionResult CreateResult(ControllerContext controllerContext)
            {
                throw new NotImplementedException();
            }
        }
    }
}
