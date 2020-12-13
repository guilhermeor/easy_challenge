using EasyChallenge.API.Presenter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyChallenge.API.Controllers
{
    [ApiController]
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
