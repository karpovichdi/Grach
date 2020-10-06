using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Grach.Core.Interfaces;

namespace Grach.Core.Extensions
{
    public static class LoggingExtensions
    {
        private const string Information = nameof(Information);

        public static Action<Exception> LogException(this ILoggingServiceProvider logger)
        {
            return exception => logger?.Error(exception.Message, exception);
        }

        [Conditional("DEBUG"), Conditional("DEBUGMOCK")]
        public static void Log(this object caller, string message = "", [CallerMemberName]string memberName = "")
        {
            string callerType = caller?.GetType().Name ?? "Caller is Null!";
            string delimiter = string.IsNullOrEmpty(message) ? string.Empty : ":";

            Debug.WriteLine($"{callerType}.{memberName}{delimiter} {message}", Information);
        }
    }
}