using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Fabrik.Common.WebAPI
{
    /// <summary>
    /// Adds JSONP support to the standard <see cref="JsonMediaTypeFormatter"/>.
    /// </summary>
    public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter
    {
        private readonly HttpRequestMessage request;
        private string callbackQueryParameter;

        public JsonpMediaTypeFormatter()
        {
            SupportedMediaTypes.Add(DefaultMediaType);
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/javascript"));

            this.AddQueryStringMapping("format", "jsonp", DefaultMediaType);
        }

        public JsonpMediaTypeFormatter(HttpRequestMessage request)
            : this()
        {
            this.request = request;
        }

        public string CallbackQueryParameter
        {
            get { return callbackQueryParameter ?? "callback"; }
            set { callbackQueryParameter = value; }
        }

        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (request == null)
                throw new ArgumentNullException("request");

            return new JsonpMediaTypeFormatter(request) { SerializerSettings = SerializerSettings };
        }

        public override Task WriteToStreamAsync(Type type, object value, Stream stream, HttpContent content, TransportContext transportContext)
        {
            string callback;
            if (IsJsonpRequest(request, out callback))
            {

                var writer = new StreamWriter(stream);
                writer.Write(callback + "(");
                writer.Flush();

                return base.WriteToStreamAsync(type, value, stream, content, transportContext).ContinueWith(_ =>
                {

                    //TODO: Inspecting the task status and acting on that is better
                    writer.Write(")");
                    writer.Flush();
                });
            }

            return base.WriteToStreamAsync(type, value, stream, content, transportContext);
        }

        private bool IsJsonpRequest(HttpRequestMessage request, out string callback)
        {
            callback = null;

            if (request == null || request.Method != HttpMethod.Get)
            {
                return false;
            }

            var query = request.RequestUri.ParseQueryString();
            callback = query[CallbackQueryParameter];

            return !string.IsNullOrEmpty(callback);
        }
    }
}
