using System.Net;

namespace Cub
{
    public class InternalServerErrorException : Exception
    {
        public InternalServerErrorException(string message, HttpStatusCode httpStatusCode, Error cubError)
            : base(message, httpStatusCode, cubError)
        {
        }
    }
}
