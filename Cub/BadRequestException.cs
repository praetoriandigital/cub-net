using System.Net;

namespace Cub
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message, HttpStatusCode httpStatusCode, Error cubError)
            : base(message, httpStatusCode, cubError)
        {
        }
    }
}
