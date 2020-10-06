using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Grach.Extensions
{
    public static class LogExtension
    {
        private const string Information = nameof(Information);

        public static void Log(this object caller, string message = "", [CallerMemberName] string memberName = "")
        {
            string callerType = caller?.GetType().Name ?? "Null caller.";

            Debug.WriteLine($"{callerType}.{memberName}: {message}", Information);
        }
    }
}