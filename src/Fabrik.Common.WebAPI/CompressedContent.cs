using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Fabrik.Common.WebAPI
{
    public class CompressedContent : HttpContent
    {
        private readonly HttpContent content;
        private readonly ICompressor compressor;

        public CompressedContent(HttpContent content, ICompressor compressor)
        {
            Ensure.Argument.NotNull(content, "content");
            Ensure.Argument.NotNull(compressor, "compressor");

            this.content = content;
            this.compressor = compressor;

            AddHeaders();
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }

        protected async override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            Ensure.Argument.NotNull(stream, "stream");

            using (content)
            {
                var contentStream = await content.ReadAsStreamAsync();
                await compressor.Compress(contentStream, stream);
            }
        }

        private void AddHeaders()
        {
            foreach (var header in content.Headers)
            {
                Headers.TryAddWithoutValidation(header.Key, header.Value);
            }

            Headers.ContentEncoding.Add(compressor.EncodingType);
        }
    }
}
