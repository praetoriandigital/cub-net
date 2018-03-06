using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json;

namespace Cub
{
    public class CubExceptionFactory
    {
        public Exception Exception(Error cubError, HttpStatusCode httpStatusCode)
        {
            var exceptionMessage = cubError.Description;

            if (cubError.Params != null && cubError.Params.Count > 0)
            {
                exceptionMessage = $"{exceptionMessage} Params: [{JsonConvert.SerializeObject(cubError.Params)}]";
            }

            if (httpStatusCode == HttpStatusCode.InternalServerError)
                return new InternalServerErrorException(exceptionMessage, httpStatusCode, cubError);

            switch ((int)httpStatusCode)
            {
                case 400:
                    return new BadRequestException(exceptionMessage, httpStatusCode, cubError);
                case 401:
                    return new UnauthorizedException(exceptionMessage, httpStatusCode, cubError);
                case 403:
                    return new ForbiddenException(exceptionMessage, httpStatusCode, cubError);
                case 404:
                    return new NotFoundException(exceptionMessage, httpStatusCode, cubError);
                case 405:
                    return new MethodNotAllowedException(exceptionMessage, httpStatusCode, cubError);
                case 500:
                    return new InternalServerErrorException(exceptionMessage, httpStatusCode, cubError);
                case 503:
                    return new ServiceUnavailableException(exceptionMessage, httpStatusCode, cubError);
                default:
                    return new Exception(exceptionMessage, httpStatusCode, cubError);
            }
        }
    }

    public class Exception : ApplicationException
    {
        public HttpStatusCode HttpStatusCode { get; set; }
        public Error UnipagError { get; set; }
        public Dictionary<string, string> Params { get; set; }

        public Exception()
        {
        }

        public override System.Collections.IDictionary Data => Params;

        public Exception(string message, HttpStatusCode httpStatusCode, Error cubError)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
            UnipagError = cubError;
            Params = cubError.Params;
        }
    }
}
