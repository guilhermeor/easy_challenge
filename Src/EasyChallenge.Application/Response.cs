using Flunt.Notifications;
using System.Net;

namespace EasyChallenge.Application
{
    public class Response<T> : Notifiable where T : struct
    {
        public Response(string message, Notification notification, HttpStatusCode statusCode)
        {
            ErrorMessage = message;
            AddNotification(notification);
            StatusCode = statusCode;
        }

        public Response(T result) => Result = result;
        public string ErrorMessage { get; set; }
        public T Result { get;}
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public static explicit operator Response<T>((string Message, Notification Notification, HttpStatusCode StatusCode) response) 
            => new(response.Message, response.Notification, response.StatusCode);
    }
}
