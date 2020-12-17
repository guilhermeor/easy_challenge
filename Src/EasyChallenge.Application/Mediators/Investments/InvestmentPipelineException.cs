using EasyChallenge.Application.Mediators.Investments;
using Flunt.Notifications;
using MediatR.Pipeline;
using OpenTracing;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EasyChallenge.Application.Mediators.Investments
{
    public class InvestmentPipelineException : IRequestExceptionHandler<InvestmentsRequest, Response<InvestmentsResponse>>
    {
        private readonly ITracer _tracer;
        public InvestmentPipelineException(ITracer tracer)
        {
            _tracer = tracer;
        }
        public Task Handle(InvestmentsRequest request, Exception exception, RequestExceptionHandlerState<Response<InvestmentsResponse>> state, CancellationToken cancellationToken)
        {
            var response = new Response<InvestmentsResponse>("Error getting Portfolio", new Notification(exception.Source, $"{Error().Message}: {exception.Message}"), Error().StatusCode);
            state.SetHandled(response);

            _tracer.ActiveSpan.SetTag("error", true);
            _tracer.ActiveSpan.SetTag("statusCode", (int)Error().StatusCode);

            return Task.CompletedTask;

            (string Message, HttpStatusCode StatusCode) Error()
                => exception.Source == "Refit" ? ("Error on External API", state.Response?.StatusCode ?? HttpStatusCode.BadRequest) : (exception.GetType().Name, HttpStatusCode.InternalServerError);
        }
    }
}
