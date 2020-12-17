using EasyChallenge.Application;
using Microsoft.AspNetCore.Mvc;

namespace EasyChallenge.API.Presenter
{
    public interface IBasePresenter
    {
        IActionResult GetActionResult<T>(Response<T> response) where T : struct;
    }
}
