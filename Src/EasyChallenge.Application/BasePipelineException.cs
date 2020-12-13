using EasyChallenge.Application.Mediators;
using Flunt.Notifications;
using MediatR.Pipeline;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace EasyChallenge.Application
{
    public class BasePipelineException : IRequestExceptionHandler<InvestmentsRequest, Response<InvestmentsResponse>>
    {
        public Task Handle(InvestmentsRequest request, Exception exception, RequestExceptionHandlerState<Response<InvestmentsResponse>> state, CancellationToken cancellationToken)
        {
            var response = new Response<InvestmentsResponse>("Error getting summary", new Notification(exception.Source, $"{Error().Message}: {exception.Message}"), Error().StatusCode);
            state.SetHandled(response);

            return Task.CompletedTask;

            (string Message, HttpStatusCode StatusCode) Error()
                => exception.Source == "Refit" ? ("Error on External API", HttpStatusCode.BadRequest) : (exception.GetType().Name, HttpStatusCode.InternalServerError);
        }
    }
}
