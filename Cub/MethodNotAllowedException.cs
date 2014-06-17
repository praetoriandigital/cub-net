using System.Net;

namespace Cub
{
    public class MethodNotAllowedException : Exception
    {
        public MethodNotAllowedException(string message, HttpStatusCode httpStatusCode, Error cubError)
            : base(message, httpStatusCode, cubError)
        {
        }
    }
}
