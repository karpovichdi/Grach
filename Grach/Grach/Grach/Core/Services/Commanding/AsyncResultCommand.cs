using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Core.Extensions;
using Grach.Core.Interfaces.Commanding;

namespace Grach.Core.Services.Commanding
{
    public class AsyncResultCommand<TResult> : AsyncCommandBase, IAsyncResultCommand<TResult>
    {
        private readonly Func<Task<TResult>> executeResult;

        public AsyncResultCommand(
            Func<Task<TResult>> executeResult,
            Func<object, bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false)
            : base(canExecute, onException, continueOnCapturedContext)
        {
            this.executeResult = executeResult;
        }

        public Task<TResult> ExecuteAsync()
        {
            return executeResult();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().SafeFireAndForget(onException, continueOnCapturedContext);
        }
    }

    public class AsyncResultCommand<TParam, TResult> : AsyncCommandBase, IAsyncResultCommand<TParam, TResult>
    {
        private readonly Func<TParam, Task<TResult>> executeResult;

        public AsyncResultCommand(
            Func<TParam, Task<TResult>> executeResult,
            Func<object, bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false)
            : base(canExecute, onException, continueOnCapturedContext)
        {
            this.executeResult = executeResult;
        }

        public Task<TResult> ExecuteAsync(TParam parameter)
        {
            return executeResult(parameter);
        }

        void ICommand.Execute(object parameter)
        {
            if (IsValidCommandParameter<TParam>(parameter))
            {
                ExecuteAsync((TParam)parameter).SafeFireAndForget(onException, continueOnCapturedContext);
            }
        }
    }
}