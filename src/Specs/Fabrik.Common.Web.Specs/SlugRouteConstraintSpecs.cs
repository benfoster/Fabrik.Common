using Machine.Specifications;
using System.Web.Routing;

namespace Fabrik.Common.Web.Specs
{
    [Subject(typeof(RegexConstraint))]
    public class SlugRouteConstraintSpecs
    {
        static IRouteConstraint regexConstraint;
        static bool result;
        
        public class When_the_parameter_is_null
        {
            Establish ctx = ()
                => regexConstraint = new SlugRouteConstraint();

            Because of = ()
                => result = regexConstraint.Match(null, null, "slug", new RouteValueDictionary(), RouteDirection.IncomingRequest);

            It Should_not_match = ()
                => result.ShouldBeFalse();
        }

        public class When_the_parameter_is_a_valid_slug
        {
            Establish ctx = ()
                => regexConstraint = new SlugRouteConstraint();

            Because of = ()
                => result = regexConstraint.Match(null, null, "slug", new RouteValueDictionary(new { slug = "aspnet-mvc" }), RouteDirection.IncomingRequest);

            It Should_match = ()
                => result.ShouldBeTrue();
        }
    }
}
