using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Core.Extensions;
using Grach.Core.Interfaces.Commanding;

namespace Grach.Core.Services.Commanding
{
    public class AsyncCommand : AsyncCommandBase, IAsyncCommand
    {
        protected readonly Func<Task> Execute;

        public AsyncCommand(
            Func<Task> execute,
            Func<object, bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false)
            : base(canExecute, onException, continueOnCapturedContext)
        {
            Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public virtual Task ExecuteAsync()
        {
            return Execute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync().SafeFireAndForget(onException, continueOnCapturedContext);
        }
    }

    public class AsyncCommand<TParam> : AsyncCommandBase, IAsyncCommand<TParam>
    {
        protected readonly Func<TParam, Task> Execute;

        public AsyncCommand(
            Func<TParam, Task> execute,
            Func<object, bool> canExecute = null,
            Action<Exception> onException = null,
            bool continueOnCapturedContext = false)
            : base(canExecute, onException, continueOnCapturedContext)
        {
            this.Execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public virtual Task ExecuteAsync(TParam parameter)
        {
            return Execute(parameter);
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