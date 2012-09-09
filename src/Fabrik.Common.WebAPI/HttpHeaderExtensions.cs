using System.Net.Http.Headers;

namespace Fabrik.Common.WebAPI
{
    public static class HttpHeaderExtensions
    {
        /// <summary>
        /// Gets the filename from the Content-Disposition header.
        /// </summary>
        /// <returns>The filename if one exists, or null if not.</returns>
        public static string GetFilename(this ContentDispositionHeaderValue disposition)
        {
            if (disposition == null || disposition.FileName == null)
                return null;

            return disposition.FileName.Replace("\"", "").Trim();
        }
    }
}
