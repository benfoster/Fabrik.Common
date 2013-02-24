using Machine.Specifications;
using System.Net;
using System.Net.Http;

namespace Fabrik.Common.WebAPI.Specs
{
    [Subject(typeof(RequireHttpsHandler))]
    public class RequireHttpsHandlerSpecs
    {
        static HttpRequestMessage request;
        static HttpResponseMessage response;
        
        public class When_the_request_is_not_over_https
        {
            Establish ctx = () => request = new HttpRequestMessage(HttpMethod.Get, "http://localhost");

            Because of = () => response = TestHelper.InvokeMessageHandler(request, new RequireHttpsHandler()).Result;

            It Should_return_a_403_forbidden_response = () => response.StatusCode.ShouldEqual(HttpStatusCode.Forbidden);
        }

        public class When_the_request_is_made_over_https
        {
            Establish ctx = () => request = new HttpRequestMessage(HttpMethod.Get, "https://localhost");

            Because of = () => response = TestHelper.InvokeMessageHandler(request, new RequireHttpsHandler()).Result;

            It Should_process_the_request_as_normal = () => response.IsSuccessStatusCode.ShouldBeTrue();
        }
    }
}
