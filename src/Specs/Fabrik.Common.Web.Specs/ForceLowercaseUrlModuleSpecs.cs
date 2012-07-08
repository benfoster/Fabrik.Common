using System;
using System.Net;
using System.Web;
using Machine.Fakes;
using Machine.Specifications;

namespace Fabrik.Common.Web.Specs
{
    [Subject(typeof(ForceLowercaseUrlModule))]
    public class ForceLowercaseUrlModuleSpecs
    {
        static HttpContextBase httpContext;
        static ForceLowercaseUrlModule module;
        
        Establish ctx = () =>
        {
            httpContext = DynamicHttpContext.Create();
            module = new ForceLowercaseUrlModule();
        };
        
        public class When_the_URL_contains_uppercase_characters
        {
            Because of = () => {
                httpContext.Request.WhenToldTo(x => x.Url).Return(new Uri("http://www.somedomain.com/Home/Index"));
                httpContext.Request.WhenToldTo(x => x.HttpMethod).Return("GET");
                module.OnBeginRequest(httpContext);
            };

            It Should_respond_with_a_301_moved_permanently_status_code = () 
                => httpContext.Response.StatusCode.ShouldEqual((int)HttpStatusCode.MovedPermanently);

            It Should_set_the_location_header_to_the_lowercase_version_of_the_URL = ()
                => httpContext.Response.RedirectLocation.ShouldEqual("http://www.somedomain.com/home/index");

            It Should_set_the_response_status_to_301_Moved_Permanently = ()
                => httpContext.Response.Status.ShouldEqual("301 Moved Permanently");
        }
    }
}
