using System;
using System.Web;

namespace Fabrik.Common.Web
{
    /// <summary>
    /// Extensions for <see cref="System.Web.HttpPostedFileBase"/>
    /// </summary>
    public static class HttpPostedFileBaseExtensions
    {
        /// <summary>
        /// Checks whether an upload is valid
        /// </summary>
        public static bool IsValid(this HttpPostedFileBase file)
        {
            return file != null && file.ContentLength > 0;
        }

        /// <summary>
        /// Validates whether an upload is an image
        /// </summary>
        public static bool IsImage(this HttpPostedFileBase file)
        {
            var contentType = MimeMapping.GetMimeMapping(file.FileName);
            return contentType.StartsWith("image", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
