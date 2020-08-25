using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Core.Interfaces.Commanding;

namespace Grach.Core.Interfaces
{
    public interface ICommandResolver
    {
        bool IsLocked { get; }

        void ForceRelease();

        #region AsyncCommands

        IAsyncCommand AsyncCommand(Func<Task> execute, bool ignoreLock = false, Func<bool> canExecute = null);

        IAsyncCommand<TParam> AsyncCommand<TParam>(Func<TParam, Task> execute, bool ignoreLock = false, Func<object, bool> canExecute = null);

        IAsyncResultCommand<TResult> AsyncResultCommand<TResult>(Func<Task<TResult>> execute, bool ignoreLock = false, Func<object, bool> canExecute = null);

        IAsyncResultCommand<TParam, TResult> AsyncResultCommand<TParam, TResult>(Func<TParam, Task<TResult>> execute, bool ignoreLock = false, Func<object, bool> canExecute = null);

        #endregion

        #region Commands

        ICommand Command(Action execute, bool ignoreLock = false, Func<bool> canExecute = null);

        ICommand Command<TParam>(Action<TParam> execute, bool ignoreLock = false, Func<object, bool> canExecute = null);

        #endregion
    }
}