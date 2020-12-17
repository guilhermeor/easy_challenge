using EasyChallenge.Application;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EasyChallenge.API.Presenter
{
    public class BasePresenter : IBasePresenter
    {
        public IActionResult GetActionResult<T>(Response<T> response) where T : struct 
            => response.Invalid ? CreateErrorResult(response) : new OkObjectResult(response.Result);

        private static IActionResult CreateErrorResult<T>(Response<T> response) where T : struct
        {
            var errorBody = new ApiError(response.ErrorMessage, response.Notifications);
            return response.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundObjectResult(errorBody),
                HttpStatusCode.UnprocessableEntity => new UnprocessableEntityObjectResult(errorBody),
                HttpStatusCode.Unauthorized => new UnauthorizedObjectResult(errorBody),
                _ => new BadRequestObjectResult(errorBody),
            };
        }
    }
}
