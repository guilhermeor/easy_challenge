using OpenTracing;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace EasyChallenge.Application.MessageHandler
{
    public class ExternalRequestHandler : DelegatingHandler
    {
        private readonly ITracer _tracer;
        public ExternalRequestHandler(ITracer tracer)
        {
            _tracer = tracer;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            var traceLogResponse = await response.Content.ReadAsStringAsync(cancellationToken);
            _tracer.ActiveSpan.Log($"Response: {traceLogResponse}");
            return response;
        }
    }
}
