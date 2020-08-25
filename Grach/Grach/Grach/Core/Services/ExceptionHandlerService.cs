using System;
using Grach.Core.Exceptions;
using Grach.Core.Interfaces;
using Refit;

namespace Grach.Core.Services
{
    public class ExceptionHandlerService : IExceptionHandler
    {
        public Exception HandleException(Exception exception)
        {
            switch (exception)
            {
                case ApiException apiException:
                {
                    return HandleApiException(apiException);
                }
                case RequestException requestException:
                {
                    return requestException;
                }
                default:
                {
                    return exception;
                }
            }
        }

        private RequestException HandleApiException(ApiException exception)
        {
            switch (exception.StatusCode)
            {
                default:
                    return new RequestException($"ErrorApiRequestGeneral Code {exception.StatusCode}", exception);
            }
        }
    }
}