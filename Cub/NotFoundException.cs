using System.Net;

namespace Cub
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message, HttpStatusCode httpStatusCode, Error cubError)
            : base(message, httpStatusCode, cubError)
        {
        }
    }
}
