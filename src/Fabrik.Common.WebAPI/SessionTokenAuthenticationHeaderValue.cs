using System.Net.Http.Headers;

namespace Fabrik.Common.WebAPI
{
    public class SessionTokenAuthenticationHeaderValue : AuthenticationHeaderValue
    {
        private const string SessionTokenSchemeName = "Session";

        public SessionTokenAuthenticationHeaderValue(string token, string scheme = SessionTokenSchemeName)
            : base(scheme, token)
        { }
    }
}
