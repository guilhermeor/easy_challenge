using EasyChallenge.API.Presenter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace EasyChallenge.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [ExcludeFromCodeCoverage]
    public class BaseController : Controller
    {
        protected readonly ISender _mediator;
        protected readonly IBasePresenter _presenter;

        public BaseController(ISender mediator, IBasePresenter basePresenter)
        {
            _presenter = basePresenter;
            _mediator = mediator;
        }
    }
}
