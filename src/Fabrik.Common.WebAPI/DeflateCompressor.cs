using System.IO;
using System.IO.Compression;

namespace Fabrik.Common.WebAPI
{
    public class DeflateCompressor : Compressor
    {
        private const string DeflateEncoding = "deflate";

        public override string EncodingType
        {
            get { return DeflateEncoding; }
        }

        public override Stream CreateCompressionStream(Stream output)
        {
            return new DeflateStream(output, CompressionMode.Compress);
        }

        public override Stream CreateDecompressionStream(Stream input)
        {
            return new DeflateStream(input, CompressionMode.Decompress);
        }
    }
}
