using Grach.Core.Interfaces;
using Grach.Core.Resources;
using Grach.Extensions;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Essentials;

namespace Grach.ViewModels.Base
{
    public class ViewModelBase : BindableModelBase, IInitialize, INavigationAware, IDestructible, IPageLifecycleAware
    {
        private string _title;

        protected IDialogService DialogService { get; }
        protected INavigationService NavigationService { get; }
        protected ILoggingServiceProvider Logger { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ViewModelBase(INavigationService navigationService,
            IDialogService dialogService,
            ILoggingServiceProvider logger)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
            Logger = logger;
        }

        public virtual void Initialize(INavigationParameters parameters) { }

        public virtual void OnNavigatedFrom(INavigationParameters parameters) { }

        public virtual void OnNavigatedTo(INavigationParameters parameters) { }

        public virtual void OnAppearing()
        {
            Connectivity.ConnectivityChanged += ConnectivityChanged;
        }

        public virtual void OnDisappearing()
        {
            Connectivity.ConnectivityChanged -= ConnectivityChanged;
        }

        public virtual void Destroy()
        {
            Connectivity.ConnectivityChanged -= ConnectivityChanged;
        }

        private async void ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            
        }
    }
}