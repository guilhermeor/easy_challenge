using EasyChallenge.Application;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyChallenge.API.Presenter
{
    public interface IBasePresenter
    {
        IActionResult GetActionResult<T>(Response<T> response) where T : struct;
    }
}
