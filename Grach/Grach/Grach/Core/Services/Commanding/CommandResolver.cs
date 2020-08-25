using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Core.Extensions;
using Grach.Core.Interfaces;
using Grach.Core.Interfaces.Commanding;
using Xamarin.Forms;

namespace Grach.Core.Services.Commanding
{
    public class CommandResolver : ICommandResolver
    {
        private readonly ICommandExecutionLock _commandExecutionLock;
        private readonly IExceptionHandler _exceptionHandler;
        private readonly ILoggingServiceProvider _logger;

        private long _lockIndex;

        public bool IsLocked => _commandExecutionLock.IsLocked;

        public CommandResolver(
            ICommandExecutionLock commandExecutionLock,
            IExceptionHandler exceptionHandler)
        {
            _commandExecutionLock = commandExecutionLock;
            _exceptionHandler = exceptionHandler;
        }

        public void ForceRelease()
        {
            Interlocked.Increment(ref _lockIndex);
            _commandExecutionLock.FreeExecutionLock();
        }

        #region AsyncCommands

        public IAsyncCommand AsyncCommand(Func<Task> execute, bool ignoreLock = false, Func<bool> canExecute = null)
        {
            async Task Execute()
            {
                await Prepare<object>(o => execute(), null, ignoreLock);
            }

            return new AsyncCommand(Execute, p => CanExecute(canExecute), _logger.LogException());
        }

        public IAsyncCommand<TParam> AsyncCommand<TParam>(Func<TParam, Task> execute, bool ignoreLock = false, Func<object, bool> canExecute = null)
        {
            async Task Func(TParam param)
            {
                await Prepare(execute, param, ignoreLock);
            }

            return new AsyncCommand<TParam>(Func, par => CanExecute(par, canExecute), _logger.LogException());
        }

        public IAsyncResultCommand<TParam, TResult> AsyncResultCommand<TParam, TResult>(Func<TParam, Task<TResult>> execute, bool ignoreLock = false, Func<object, bool> canExecute = null)
        {
            async Task<TResult> Func(TParam param)
            {
                return await Prepare(execute, param, ignoreLock);
            }

            return new AsyncResultCommand<TParam, TResult>(Func, par => CanExecute(par, canExecute), _logger.LogException());
        }

        public IAsyncResultCommand<TResult> AsyncResultCommand<TResult>(Func<Task<TResult>> execute, bool ignoreLock = false, Func<object, bool> canExecute = null)
        {
            async Task<TResult> Func()
            {
                return await Prepare<object, TResult>(o => execute(), null, ignoreLock);
            }

            return new AsyncResultCommand<TResult>(Func, par => CanExecute(par, canExecute), _logger.LogException());
        }

        #endregion

        #region Commands

        public ICommand Command(Action execute, bool lockExecution = true, Func<bool> canExecute = null)
        {
            return Command<object>(o => execute(), lockExecution, o => CanExecute(canExecute));
        }

        public ICommand Command<TParam>(Action<TParam> execute, bool ignoreLock = false, Func<object, bool> canExecute = null)
        {
            void Action(TParam param)
            {
                Prepare(execute, param, ignoreLock);
            }

            return new Command<TParam>(Action, par => CanExecute(par, canExecute));
        }

        #endregion

        #region CanExecute

        private static bool CanExecute<TParam>(TParam par, Func<object, bool> canExecute = null)
        {
            return canExecute == null || canExecute(par);
        }

        private static bool CanExecute(Func<bool> canExecute = null)
        {
            return canExecute == null || canExecute();
        }

        #endregion CanExecute

        #region Prepare

        private async Task Prepare<TParam>(Func<TParam, Task> execute, TParam param, bool ignoreLock)
        {
            if (!ignoreLock && IsLocked) return;

            long currentLockIndex = 0;
            try
            {
                if (!ignoreLock && !_commandExecutionLock.TryLockExecution()) return;

                currentLockIndex = Interlocked.Increment(ref _lockIndex);
                await Execute(execute, param);
            }
            finally
            {
                if (Interlocked.Read(ref _lockIndex) == currentLockIndex)
                    _ = _commandExecutionLock.FreeExecutionLock();
            }
        }

        private async Task<TResult> Prepare<TParam, TResult>(Func<TParam, Task<TResult>> execute, TParam param, bool ignoreLock)
        {
            if (!ignoreLock && IsLocked)
                return default(TResult);

            long currentLockIndex = 0;
            try
            {
                if (!ignoreLock && !_commandExecutionLock.TryLockExecution())
                    return default(TResult);

                currentLockIndex = Interlocked.Increment(ref _lockIndex);
                return await Execute(execute, param);
            }
            finally
            {
                if (Interlocked.Read(ref _lockIndex) == currentLockIndex)
                    _ = _commandExecutionLock.FreeExecutionLock();
            }
        }

        private void Prepare<TParam>(Action<TParam> execute, TParam param, bool ignoreLock)
        {
            if (!ignoreLock && IsLocked) return;

            try
            {
                if (!ignoreLock && !_commandExecutionLock.TryLockExecution()) return;

                Execute(execute, param);
            }
            finally
            {
                _commandExecutionLock.FreeExecutionLock();
            }
        }

        #endregion Prepare

        #region Execute

        private async Task Execute<TParam>(Func<TParam, Task> execute, TParam param)
        {
            try
            {
                await execute(param);
            }
            catch (Exception e)
            {
                if (_exceptionHandler.HandleException(e) is { } handledException)
                {
                    throw handledException;
                }
            }
        }

        private async Task<TResult> Execute<TParam, TResult>(Func<TParam, Task<TResult>> execute, TParam param)
        {
            try
            {
                return await execute(param);
            }
            catch (Exception e)
            {
                if (_exceptionHandler.HandleException(e) is { } handledException)
                {
                    throw handledException;
                }

                return default(TResult);
            }
        }

        private void Execute<TParam>(Action<TParam> execute, TParam param)
        {
            try
            {
                execute(param);
            }
            catch (Exception e)
            {
                if (_exceptionHandler.HandleException(e) is { } handledException)
                {
                    throw handledException;
                }
            }
        }

        #endregion
    }
}