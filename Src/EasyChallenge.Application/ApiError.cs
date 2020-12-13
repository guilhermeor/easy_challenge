using Flunt.Notifications;
using System.Collections.Generic;
using System.Linq;

namespace EasyChallenge.Application
{
    public readonly struct ApiError
    {
        public string Message { get; }
        public IEnumerable<string> Errors { get; }

        public ApiError(string message, IReadOnlyCollection<Notification> errors)
        {
            Message = message;
            Errors = errors.Select(x => x.Message);
        }
    }
}
