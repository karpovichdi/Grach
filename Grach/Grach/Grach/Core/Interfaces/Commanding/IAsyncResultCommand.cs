using System.Threading.Tasks;
using System.Windows.Input;

namespace Grach.Core.Interfaces.Commanding
{
    public interface IAsyncResultCommand<TResult> : ICommand
    {
        Task<TResult> ExecuteAsync();
    }

    public interface IAsyncResultCommand<TParam, TResult> : ICommand
    {
        Task<TResult> ExecuteAsync(TParam parameter);
    }
}