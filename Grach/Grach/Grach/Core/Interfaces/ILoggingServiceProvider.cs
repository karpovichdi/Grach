using System;
using System.Collections.Generic;

namespace Grach.Core.Interfaces
{
    public interface ILoggingServiceProvider
    {
        void Debug(string msg, Dictionary<string, object> additionalInfo = null);

        void Info(string msg, Dictionary<string, object> additionalInfo = null);

        void Warn(string msg, Dictionary<string, object> additionalInfo = null);

        void Error(string msg, Exception exception = null, Dictionary<string, object> additionalInfo = null);
    }
}