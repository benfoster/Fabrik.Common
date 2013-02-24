using System;
using System.Net.Http.Headers;
using System.Text;

namespace Fabrik.Common.WebAPI
{
    public class ApiKeyAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        private const string ApiKeySchemeName = "ApiKey";

        public ApiKeyAuthenticationHeaderValue(string apiKey, string scheme = ApiKeySchemeName)
            : base(scheme, EncodeKey(apiKey))
        { }

        private static string EncodeKey(string apiKey)
        {
            Ensure.Argument.NotNullOrEmpty(apiKey, "apiKey");
            var encoding = Encoding.GetEncoding("iso-8859-1");
            return Convert.ToBase64String(encoding.GetBytes(apiKey));
        }
    }
}
