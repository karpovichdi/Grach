using System;
using System.Threading.Tasks;

namespace Grach.Core.Extensions
{
    public static class TaskExtension
    {
        public static async void SafeFireAndForget(this Task task, Action<Exception> onException, bool continueOnCapturedContext = false)
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (Exception exception) when (onException != null)
            {
                onException.Invoke(exception);
            }
        }
    }
}