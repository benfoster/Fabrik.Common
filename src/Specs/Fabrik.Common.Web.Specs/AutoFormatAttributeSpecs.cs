using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using System.Web.Mvc;
using Machine.Fakes;

namespace Fabrik.Common.Web.Specs
{
    [Subject(typeof(AutoFormatResultAttribute))]
    public class AutoFormatAttributeSpecs
    {
        static AutoFormatResultAttribute attribute;
        static ActionExecutedContext filterContext;
        
        public class When_no_formatters_are_satisfied : WithFakes
        {
            Establish ctx = () =>
            {
                filterContext = An<ActionExecutedContext>();
                attribute = new AutoFormatResultAttribute();
            };

            Because of = ()
                => attribute.OnActionExecuted(filterContext);

            It Should_not_change_the_result = ()
                => filterContext.Result.ShouldBeOfType<EmptyResult>();
        }

        public class When_a_formatter_is_satisfied_and_returns_null : WithFakes
        {
            static IViewResultFormatter formatter;
            
            Establish ctx = () => {
                filterContext = An<ActionExecutedContext>();
                attribute = new AutoFormatResultAttribute();

                formatter = An<IViewResultFormatter>();
                formatter.WhenToldTo(x => x.IsSatisfiedBy(Param.IsAny<ControllerContext>()))
                    .Return(true);

                formatter.WhenToldTo(x => x.CreateResult(Param.IsAny<ControllerContext>(), Param.IsAny<ActionResult>()))
                    .Return(() => null);

                ViewResultFormatters.Formatters.Clear();
                ViewResultFormatters.Formatters.Add(formatter);
            };

            Because of = ()
                => attribute.OnActionExecuted(filterContext);

            It Should_ask_the_formatter_to_create_the_result = ()
                => formatter.WasToldTo(x => x.CreateResult(Param.IsAny<ControllerContext>(), Param.IsAny<ActionResult>()));

            It Should_not_format_the_result = ()
                => filterContext.Result.ShouldBeOfType<EmptyResult>();
        }

        public class When_a_formatter_is_satisfied_and_returns_a_result : WithFakes
        {
            static IViewResultFormatter formatter;

            Establish ctx = () =>
            {
                filterContext = An<ActionExecutedContext>();
                attribute = new AutoFormatResultAttribute();

                formatter = An<IViewResultFormatter>();
                formatter.WhenToldTo(x => x.IsSatisfiedBy(Param.IsAny<ControllerContext>()))
                    .Return(true);

                formatter.WhenToldTo(x => x.CreateResult(Param.IsAny<ControllerContext>(), Param.IsAny<ActionResult>()))
                    .Return(() => new ContentResult());

                ViewResultFormatters.Formatters.Clear();
                ViewResultFormatters.Formatters.Add(formatter);
            };

            Because of = ()
                => attribute.OnActionExecuted(filterContext);

            It Should_format_the_result = ()
                => filterContext.Result.ShouldBeOfType<ContentResult>();
        }
    }
}
