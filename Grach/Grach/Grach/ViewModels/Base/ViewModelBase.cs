using System.Threading.Tasks;
using Grach.Interfaces;
using Prism.Mvvm;
using Prism.Navigation;

namespace Grach.ViewModels.Base
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible, IBackNavigationHandler
    {
        private string _title;

        protected INavigationService NavigationService { get; private set; }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Initialize(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual Task<INavigationResult> NavigateBack(INavigationParameters parameters = null, bool? modalNavigation = null, bool animated = true)
        {
            return NavigationService.GoBackAsync(parameters, modalNavigation, animated);
        }

        public virtual void Destroy() { }
    }
}