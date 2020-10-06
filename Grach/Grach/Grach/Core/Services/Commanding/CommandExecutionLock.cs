using System.Threading.Tasks;
using Grach.Core.Interfaces;
using Grach.Core.Interfaces.Commanding;

namespace Grach.Core.Services.Commanding
{
    public class CommandExecutionLock : ICommandExecutionLock
    {
        /// <summary>
        ///     This interval is necessary to avoid multi tapping command from the user
        ///     It can happen when user clicks simultaneously on several buttons on the screen
        /// </summary>
        public static int CommandExecutionInterval = 300;

        private readonly object _lockObject;

        private bool _isExecutionLock;

        public CommandExecutionLock()
        {
            _lockObject = new object();
            _isExecutionLock = false;
        }

        private bool IsLockedImplementationNonBlocked => _isExecutionLock;

        public bool IsLocked
        {
            get
            {
                lock (_lockObject)
                {
                    return IsLockedImplementationNonBlocked;
                }
            }
        }

        public bool TryLockExecution()
        {
            if (IsLockedImplementationNonBlocked) return false;

            lock (_lockObject)
            {
                if (IsLockedImplementationNonBlocked) return false;

                return _isExecutionLock = true;
            }
        }

        public async Task<bool> FreeExecutionLock()
        {
            await Task.Delay(CommandExecutionInterval);
            if (!IsLockedImplementationNonBlocked) return false;

            lock (_lockObject)
            {
                if (!IsLockedImplementationNonBlocked) return false;

                _isExecutionLock = false;
                return true;
            }
        }
    }
}