using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;
using NSubstitute;

namespace Fabrik.Common.Web.Specs
{
    public static class DynamicHttpContext
    {
        public static HttpContextBase Create(string requestUrl = "~/")
        {
            var httpContext = Substitute.For<HttpContextBase>();
            var request = SetupRequest(requestUrl);
            httpContext.Request.Returns(request);

            var response = SetupResponse();
            httpContext.Response.Returns(response);

            return httpContext;
        }

        private static HttpRequestBase SetupRequest(string requestUrl)
        {
            var request = Substitute.For<HttpRequestBase>();
            var browser = Substitute.For<HttpBrowserCapabilitiesBase>();
            var form = new NameValueCollection();
            var queryString = new NameValueCollection();
            var cookies = new HttpCookieCollection();
            var serverVariables = new NameValueCollection();
            var headers = new NameValueCollection();

            request.AppRelativeCurrentExecutionFilePath.Returns(requestUrl);
            request.Form.Returns(form);
            request.QueryString.Returns(queryString);
            request.Cookies.Returns(cookies);
            request.ServerVariables.Returns(serverVariables);
            request.Params.Returns(ctx => CreateParams(queryString, form, cookies, serverVariables));
            request.Browser.Returns(browser);
            request.Headers.Returns(headers);

            return request;
        }

        private static NameValueCollection CreateParams(NameValueCollection queryString, NameValueCollection form, HttpCookieCollection cookies, NameValueCollection serverVariables)
        {
            NameValueCollection parms = new NameValueCollection(48);
            parms.Add(queryString);
            parms.Add(form);
            for (var i = 0; i < cookies.Count; i++)
            {
                var cookie = cookies.Get(i);
                parms.Add(cookie.Name, cookie.Value);
            }
            parms.Add(serverVariables);
            return parms;
        }
        
        private static HttpResponseBase SetupResponse()
        {
            var sb = new StringBuilder();
            var writer = new StringWriter(sb);
            var response = Substitute.For<HttpResponseBase>();
            response.ApplyAppPathModifier(Arg.Any<string>()).Returns(ctx => ctx.Arg<string>());
            response.OutputStream.Returns(new MemoryStream());
            response.Output.Returns(writer);
            response.When(x => x.Write(Arg.Any<string>())).Do(ctx => writer.Write(ctx.Arg<string>()));
            return response;
        }  
     
        public static void SetupAjaxRequest(this HttpContextBase httpContext) 
        {
            httpContext.Request["X-Requested-With"].Returns("XMLHttpRequest");
        }
    }
}
