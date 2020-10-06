using System.Threading.Tasks;

namespace Grach.Core.Interfaces.Commanding
{
    public interface ICommandExecutionLock
    {
        bool IsLocked { get; }

        bool TryLockExecution();

        Task<bool> FreeExecutionLock();
    }
}