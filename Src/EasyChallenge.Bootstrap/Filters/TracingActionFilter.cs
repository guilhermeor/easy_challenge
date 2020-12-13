using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenTracing;
using OpenTracing.Propagation;
using System;
using System.Linq;
using System.Net;

namespace EasyChallenge.Bootstrap.Filters
{
    public class TracingActionFilter : IActionFilter
    {
        private readonly ITracer _tracer;
        private readonly ILogger<TracingActionFilter> _logger;
        public TracingActionFilter(ITracer tracer, ILogger<TracingActionFilter> logger)
        {
            _tracer = tracer;
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var operationName = context.HttpContext.Request.Path.Value;
            var spanBuilder = _tracer.BuildSpan(operationName);
            var scope = spanBuilder.StartActive();
            _logger.LogInformation($"ReceiveMessage: {DateTime.UtcNow} | {operationName} SpanId:{scope.Span.Context.SpanId} TraceId:{scope.Span.Context.TraceId}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var scope = _tracer.ScopeManager.Active;

            if (context.HttpContext.Response.StatusCode != (int)HttpStatusCode.OK)
                _tracer.ActiveSpan?.Log("Request unsuccessful!");
            else
                _tracer.ActiveSpan?.Log("Request successful!");

            var resultData = JsonConvert.SerializeObject(context.Result);

            scope.Span.SetTag("ResultData", resultData).Finish();
        }

    }

}
