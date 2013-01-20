using System;
using System.Net.Http;

namespace Fabrik.Common.WebAPI
{
    /// <summary>
    /// A base class for response enrichers that require access to the response object content.
    /// </summary>
    public abstract class ObjectContentResponseEnricher<T> : IResponseEnricher
    {
        private HttpResponseMessage httpResponse;
        
        public virtual bool CanEnrich(Type contentType)
        {
            return contentType == typeof(T);
        }

        public abstract void Enrich(T content);

        bool IResponseEnricher.CanEnrich(HttpResponseMessage response)
        {
            var content = response.Content as ObjectContent;
            return (content != null && CanEnrich(content.ObjectType));
        }

        HttpResponseMessage IResponseEnricher.Enrich(HttpResponseMessage response)
        {
            T content;
            if (response.TryGetContentValue(out content))
            {
                Enrich(content);
            }

            httpResponse = response;
            return response;
        }

        protected HttpResponseMessage HttpResponse
        {
            get { return httpResponse; }
        }
    }
}
