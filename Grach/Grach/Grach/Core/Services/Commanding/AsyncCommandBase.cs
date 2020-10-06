using System;
using System.Reflection;

namespace Grach.Core.Services.Commanding
{
    public abstract class AsyncCommandBase
    {
        protected readonly Func<object, bool> canExecute;
        protected readonly Action<Exception> onException;
        protected readonly bool continueOnCapturedContext;
        protected readonly WeakEventManager weakEventManager = new WeakEventManager();

        protected AsyncCommandBase(
            Func<object, bool> canExecute,
            Action<Exception> onException,
            bool continueOnCapturedContext)
        {
            this.canExecute = canExecute;
            this.onException = onException;
            this.continueOnCapturedContext = continueOnCapturedContext;

            weakEventManager = new WeakEventManager();
        }

        public event EventHandler CanExecuteChanged
        {
            add { weakEventManager.AddEventHandler(value); }
            remove { weakEventManager.RemoveEventHandler(value); }
        }

        public bool CanExecute(object parameter)
        {
            return canExecute?.Invoke(parameter) ?? true;
        }

        public void RaiseCanExecuteChanged()
        {
            weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));
        }

        protected static bool IsValidCommandParameter<TParam>(object parameter)
        {
            if (parameter != null)
            {
                var num = parameter is TParam;
                if (!num)
                {
                    throw new ArgumentException($"{typeof(TParam)}, {parameter.GetType()}");
                }
                return num;
            }

            Type typeFromHandle = typeof(TParam);
            if (Nullable.GetUnderlyingType(typeFromHandle) != null)
            {
                return true;
            }

            var num2 = !typeFromHandle.GetTypeInfo().IsValueType;
            if (!num2)
            {
                throw new ArgumentException($"{typeof(TParam)}");
            }
            return num2;
        }
    }
}