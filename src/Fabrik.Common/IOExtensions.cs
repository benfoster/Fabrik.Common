using System.IO;

namespace Fabrik.Common
{
    /// <summary>
    /// Extensions for various classes in the <see cref="System.IO"/> namespace.
    /// </summary>
    public static class IOExtensions
    {
        /// <summary>
        /// Reads the input <paramref name="stream"/> into a byte array.
        /// </summary>
        /// <param name="stream">The input stream to read.</param>
        /// <returns>A byte array with the contents of the input <paramref name="stream"/>.</returns>
        public static byte[] ReadFully(this Stream stream)
        {
            Ensure.Argument.NotNull(stream, "stream");
            
            if (stream is Memory)
                return (stream as Memory).ToArray();
            
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Checks whether the provided file system object is hidden.
        /// </summary>
        public static bool IsHidden(this FileSystemInfo fsi)
        {
            return fsi.Attributes.HasFlag(FileAttributes.Hidden);
        }
    }
}
