using Machine.Specifications;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Fabrik.Common.WebAPI.Specs
{
    [Subject(typeof(DecompressionHandler))]
    public class DecompressionHandlerSpecs
    {
        static HttpClient client;
        static HttpResponseMessage response;

        public class When_the_response_is_compressed
        {
            Establish ctx = () =>
            {
                var config = new HttpConfiguration();
                config.Routes.MapHttpRoute(name: "Default", 
                    routeTemplate: "{controller}/{id}", defaults: new { controller = "values", id = RouteParameter.Optional });
                
                config.MessageHandlers.Add(new CompressionHandler());
                
                var server = new HttpServer(config);

                var handler = new DecompressionHandler
                {
                    InnerHandler = server
                };

                client = new HttpClient(handler);               
            };

            Because of = () =>
            {
                var request = new HttpRequestMessage
                {
                    RequestUri = new Uri("http://localhost/contacts"),
                };

                request.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                response = client.SendAsync(request).Result;
            };

            It Should_decompress_the_content = () =>
            {
                var contacts = response.Content.ReadAsAsync<IEnumerable<Contact>>().Result;
                contacts.ShouldNotBeEmpty();
            };

            It Should_set_the_content_type = () =>
                response.Content.Headers.ContentType.MediaType.ShouldEqual("application/json");
        }
    }
   
}
