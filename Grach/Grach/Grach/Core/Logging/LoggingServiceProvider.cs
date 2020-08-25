using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grach.Core.Enums;
using Grach.Core.Interfaces;

namespace Grach.Core.Logging
{
    public class LoggingServiceProvider : ILoggingServiceProvider
    {
        private IEnumerable<ILoggingService> _backupServices;
        private readonly IEnumerable<ILoggingService> _services;

        public LoggingServiceProvider(params ILoggingService[] services)
        {
            _services = services;

            AttachErrorEvents();
        }

        public void Debug(string msg, Dictionary<string, object> additionalInfo = null)
        {
            Log(LoggingLevels.Debug, msg, additionalInfo: additionalInfo);
        }

        public void Info(string msg, Dictionary<string, object> additionalInfo = null)
        {
            Log(LoggingLevels.Info, msg, additionalInfo: additionalInfo);
        }

        public void Warn(string msg, Dictionary<string, object> additionalInfo = null)
        {
            Log(LoggingLevels.Warn, msg, additionalInfo: additionalInfo);
        }

        public void Error(string msg, Exception exception = null, Dictionary<string, object> additionalInfo = null)
        {
            Log(LoggingLevels.Error, msg, exception, additionalInfo);
        }

        private void AttachErrorEvents()
        {
            foreach (var service in _services)
            {
                service.OnError += (exception, logs) =>
                {
                    HandleLoggingError(service, exception);
                };
            }
        }

        private void Log(LoggingLevels level, string msg, Exception exception = null, Dictionary<string, object> additionalInfo = null)
        {
            var actualServices = _services.Where(s => s.Level <= level);
            foreach (var service in actualServices)
            {
                LogMessage(service, msg, level, exception, additionalInfo);
            }
        }

        private void LogMessage(ILoggingService service, string msg, LoggingLevels level, Exception exception = null, Dictionary<string, object> additionalInfo = null, bool backup = false)
        {
            Action logAction = () =>
            {
                try
                {
                    service.Log(msg, level, exception, additionalInfo);
                }
                catch (Exception ex)
                {
                    if (!backup)
                    {
                        HandleLoggingError(service, ex);
                    }
                }
            };

            if (service.RunInBackground)
            {
                Task.Factory.StartNew(logAction);
            }
            else
            {
                logAction();
            }
        }

        private void HandleLoggingError(ILoggingService service, Exception exception)
        {
            _backupServices = _services.Where(s => s.Backup);

            foreach (var backupService in _backupServices)
            {
                LogMessage(backupService, $"Unable to log report to {service.GetType().Name}", LoggingLevels.Error, exception, backup: true);
            }
        }
    }
}