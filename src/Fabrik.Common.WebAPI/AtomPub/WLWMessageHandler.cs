using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Fabrik.Common.WebAPI.AtomPub
{
    public class WLWMessageHandler : DelegatingHandler
    {
        private const string WLWUserAgent = "Windows Live Writer 1.0";

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.Headers.UserAgent != null &&
                request.Headers.UserAgent.Any(a => a.Comment != null && a.Comment.Contains(WLWUserAgent)))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/atom+xml"));
            }

            return base.SendAsync(request, cancellationToken);
        }
    }
}
