using System.Threading.Tasks;

using Prism.Navigation;

namespace Grach.Interfaces
{
    public interface IBackNavigationHandler
    {
        Task<INavigationResult> NavigateBack(INavigationParameters parameters=null, bool? modalNavigation=null, bool animated=true);
    }
}