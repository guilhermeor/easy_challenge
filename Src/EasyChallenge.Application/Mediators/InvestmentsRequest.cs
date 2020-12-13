using EasyChallenge.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChallenge.Application.Mediators
{
    public readonly struct InvestmentsRequest : IRequest<Response<InvestmentsResponse>>
    {
    }
}
