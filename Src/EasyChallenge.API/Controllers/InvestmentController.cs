using EasyChallenge.API.Presenter;
using EasyChallenge.Application.Mediators;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyChallenge.API.Controllers
{
    public class InvestmentController : BaseController
    {
        public InvestmentController(ISender mediator, IBasePresenter basePresenter)
            : base(mediator, basePresenter) { }

        [HttpGet("summary")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvestmentsResponse))]
        public async Task<IActionResult> Get()
            => _presenter.GetActionResult(await _mediator.Send(new InvestmentsRequest()));
    }
}
