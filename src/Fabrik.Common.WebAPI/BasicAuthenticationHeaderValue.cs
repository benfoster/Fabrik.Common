using System;
using System.Net.Http.Headers;
using System.Text;

namespace Fabrik.Common.WebAPI
{
    public class BasicAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        private const string BasicSchemeName = "Basic";

        public BasicAuthenticationHeaderValue(string userName, string password, string scheme = BasicSchemeName)
            : base(scheme, EncodeCredential(userName, password))
        { }

        private static string EncodeCredential(string userName, string password)
        {
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var credential = "{0}:{1}".FormatWith(userName, password);
            return Convert.ToBase64String(encoding.GetBytes(credential));
        }
    }
}
