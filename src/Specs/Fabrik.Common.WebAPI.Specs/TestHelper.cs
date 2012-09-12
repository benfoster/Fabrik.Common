using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Fabrik.Common.WebAPI.Specs
{
    /// <summary>
    /// Helper for invoking handlers
    /// </summary>
    /// <remarks>
    /// Credit Tugberk Ugurlu
    /// </remarks>
    public static class TestHelper
    {
        internal static Task<HttpResponseMessage> InvokeMessageHandler(HttpRequestMessage request, DelegatingHandler handler, CancellationToken cancellationToken = default(CancellationToken))
        {
            handler.InnerHandler = new DummyHandler();
            var invoker = new HttpMessageInvoker(handler);
            return invoker.SendAsync(request, cancellationToken);
        }

        private class DummyHandler : DelegatingHandler
        {
            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                return Task.Factory.StartNew(() => new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("Test") });
            }
        }
    }
}
