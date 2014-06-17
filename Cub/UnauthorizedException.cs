using System.Net;

namespace Cub
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string message, HttpStatusCode httpStatusCode, Error cubError)
            : base(message, httpStatusCode, cubError)
        {
        }
    }
}
