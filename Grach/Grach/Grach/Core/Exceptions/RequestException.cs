using System;

namespace Grach.Core.Exceptions
{
    public class RequestException : Exception
    {
        public new Exception InnerException { get; }

        public RequestException(string message)
            : base(message) { }

        public RequestException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}