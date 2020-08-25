using System;
using System.Collections.Generic;
using System.Diagnostics;
using Grach.Core.Enums;
using Grach.Core.Extensions;
using Grach.Core.Interfaces;
using Grach.Core.Models;

namespace Grach.Core.Services
{
    public class ConsoleLoggingService : ILoggingService
    {
        public LoggingLevels Level => LoggingLevels.Debug;

        public bool RunInBackground => false;

        public bool Backup => true;

        public event Action<Exception, IList<Log>> OnError;

        public void Log(string msg, LoggingLevels level, Exception exception = null, IDictionary<string, object> additionalInfo = null)
        {
            InternalLog(msg, level, additionalInfo);

            if (exception != null)
            {
                Debug.WriteLine(exception.ToString());
            }
        }

        protected void InternalLog(string msg, LoggingLevels level, IDictionary<string, object> additionalInfo)
        {
            Debug.WriteLine($"{msg} - {additionalInfo.ToKeysAndValuesString()}".TrimEnd(' ', '-'), $"[{level.ToString().ToUpper()}]");
        }
    }
}