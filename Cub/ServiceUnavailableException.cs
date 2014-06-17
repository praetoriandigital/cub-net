using System.Net;

namespace Cub
{
    public class ServiceUnavailableException : Exception
    {
        public ServiceUnavailableException(string message, HttpStatusCode httpStatusCode, Error cubError)
            : base(message, httpStatusCode, cubError)
        {
        }
    }
}
