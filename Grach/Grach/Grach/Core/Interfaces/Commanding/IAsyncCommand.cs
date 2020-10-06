using System.Threading.Tasks;
using System.Windows.Input;

namespace Grach.Core.Interfaces.Commanding
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
    }

    public interface IAsyncCommand<TParam> : ICommand
    {
        Task ExecuteAsync(TParam parameter);
    }
}