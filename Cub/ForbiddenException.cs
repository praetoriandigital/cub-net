using System.Net;

namespace Cub
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException(string message, HttpStatusCode httpStatusCode, Error cubError)
            : base(message, httpStatusCode, cubError)
        {
        }
    }
}
