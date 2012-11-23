using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Fabrik.Common.WebAPI
{
    public class CompressionHandler : DelegatingHandler
    {
        public Collection<ICompressor> Compressors { get; private set; }

        public CompressionHandler()
        {
            Compressors = new Collection<ICompressor>();
            
            Compressors.Add(new GZipCompressor());
            Compressors.Add(new DeflateCompressor());
        }
        
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (request.Headers.AcceptEncoding.IsNotNullOrEmpty() && response.Content != null)
            {
                var encoding = request.Headers.AcceptEncoding.First();

                var compressor = Compressors.FirstOrDefault(c => 
                    ((encoding.Quality ?? 1.0) > 0) && c.EncodingType.Equals(encoding.Value, StringComparison.InvariantCultureIgnoreCase));

                if (compressor != null)
                {
                    response.Content = new CompressedContent(response.Content, compressor);
                }
            }

            return response;
        }       
    }
}
