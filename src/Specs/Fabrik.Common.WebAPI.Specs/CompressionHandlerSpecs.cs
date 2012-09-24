using Machine.Specifications;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Fabrik.Common.WebAPI.Specs
{
    [Subject(typeof(CompressionHandler))]
    public class CompressionHandlerSpecs
    {
        static DelegatingHandler handler;
        static HttpRequestMessage request;
        static HttpResponseMessage response;
        
        public class When_the_request_contains_a_valid_encoding_type
        {           
            Establish ctx = () =>
            {
                request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/");
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                handler = new CompressionHandler();
            };

            Because of = () =>
            {
                response = TestHelper.InvokeMessageHandler(request, handler).Result;
            };

            It Should_compress_the_response = ()
                => response.Content.ShouldBeOfType(typeof(CompressedContent));

            It Should_set_the_content_encoding_header = ()
                => response.Content.Headers.ContentEncoding.ShouldContain("gzip");
        }

        public class When_the_request_contains_an_invalid_encoding_type
        {
            Establish ctx = () =>
            {
                request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/");
                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("foobar"));
                handler = new CompressionHandler();
            };

            Because of = () =>
            {
                response = TestHelper.InvokeMessageHandler(request, handler).Result;
            };
            
            It Should_return_the_original_content = ()
                 => response.Content.ShouldBeOfType(typeof(StringContent));
        }

        public class When_the_request_does_not_request_encoding
        {
            Establish ctx = () =>
            {
                request = new HttpRequestMessage(HttpMethod.Get, "http://localhost/");
                handler = new CompressionHandler();
            };

            Because of = () =>
            {
                response = TestHelper.InvokeMessageHandler(request, handler).Result;
            };

            It Should_return_the_original_content = ()
                 => response.Content.ShouldBeOfType(typeof(StringContent));
        }
    }
}
