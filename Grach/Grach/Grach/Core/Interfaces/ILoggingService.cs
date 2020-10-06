using System;
using System.Collections.Generic;
using Grach.Core.Enums;
using Grach.Core.Models;

namespace Grach.Core.Interfaces
{
    public interface ILoggingService
    {
        LoggingLevels Level { get; }

        bool RunInBackground { get; }

        bool Backup { get; }

        void Log(string msg, LoggingLevels level, Exception exception = null, IDictionary<string, object> additionalInfo = null);

        event Action<Exception, IList<Log>> OnError;
    }
}