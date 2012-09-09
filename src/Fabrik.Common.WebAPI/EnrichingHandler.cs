using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Fabrik.Common.WebAPI
{
    public class EnrichingHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {
                    var response = task.Result;
                    var enrichers = request.GetConfiguration().GetResponseEnrichers();

                    return enrichers.Where(e => e.CanEnrich(response))
                        .Aggregate(response, (resp, enricher) => enricher.Enrich(response));
                });
        }
    }
}
