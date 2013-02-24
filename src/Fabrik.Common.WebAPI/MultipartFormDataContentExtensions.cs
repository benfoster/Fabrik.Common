using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Routing;

namespace Fabrik.Common.WebAPI
{
    /// <summary>
    /// Extensions for <see cref="System.Net.Http.MultipartFormDataContent"/>.
    /// </summary>
    public static class MultipartFormDataContentExtensions
    {
        public static void Add(this MultipartFormDataContent form, HttpContent content, object formValues)
        {
            Ensure.Argument.NotNull(form, "form");
            Ensure.Argument.NotNull(content, "content");
            Ensure.Argument.NotNull(formValues, "formValues");

            Add(form, content, formValues);
        }

        public static void Add(this MultipartFormDataContent form, HttpContent content, string name, object formValues)
        {
            Ensure.Argument.NotNull(form, "form");
            Ensure.Argument.NotNull(content, "content");
            Ensure.Argument.NotNullOrEmpty(name, "name");
            Ensure.Argument.NotNull(formValues, "formValues");

            Add(form, content, formValues, name: name);
        }

        public static void Add(this MultipartFormDataContent form, HttpContent content, string name, string fileName, object formValues)
        {
            Ensure.Argument.NotNull(form, "form");
            Ensure.Argument.NotNull(content, "content");
            Ensure.Argument.NotNullOrEmpty(name, "name");
            Ensure.Argument.NotNullOrEmpty(fileName, "fileName");
            Ensure.Argument.NotNull(formValues, "formValues");

            Add(form, content, formValues, name: name, fileName: fileName);
        }

        private static void Add(this MultipartFormDataContent form, HttpContent content, object formValues, string name = null, string fileName = null)
        {
            var header = new ContentDispositionHeaderValue("form-data");
            header.Name = name;
            header.FileName = fileName;
            header.FileNameStar = fileName;

            var headerParameters = new HttpRouteValueDictionary(formValues);
            foreach (var parameter in headerParameters)
            {
                header.Parameters.Add(parameter.Value != null
                    ? new NameValueHeaderValue(parameter.Key, parameter.Value.ToString())
                    : new NameValueHeaderValue(parameter.Key));
            }

            content.Headers.ContentDisposition = header;
            form.Add(content);
        }
    }
}
