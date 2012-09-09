using System;
using System.Collections.Specialized;
using System.Net.Http;

namespace Fabrik.Common.WebAPI
{
    /// <summary>
    /// Extensions for <see cref="System.Uri"/>.
    /// </summary>
    public static class UriExtensions
    {
        /// <summary>
        /// Applies the specified <paramref name="modification"/> to the <paramref name="uri"/> querystring.
        /// </summary>
        /// <returns>An absolute URI with modified querystring</returns>
        public static string WithModifiedQuerystring(this Uri uri, Action<NameValueCollection> modification)
        {
            Ensure.Argument.NotNull(uri, "uri");
            Ensure.Argument.NotNull(modification, "modification");

            var query = uri.ParseQueryString();
            modification(query);
            return "{0}?{1}".FormatWith(uri.GetLeftPart(UriPartial.Path), query.ToString());
        }
    }
}
