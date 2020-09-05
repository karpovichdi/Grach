using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Core.Interfaces;
using Grach.Core.Resources;
using Grach.Extensions;
using Prism.AppModel;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Grach.ViewModels.Base
{
    public class ViewModelBase : BindableModelBase, IInitialize, INavigationAware, IDestructible, IPageLifecycleAware
    {
        private string _title;
        
        private CancellationTokenSource _cancellationTokenSource;

        protected IDialogService DialogService { get; }
        protected INavigationService NavigationService { get; }
        protected ILoggingServiceProvider Logger { get; }
        
        protected ICommand LogOutCommand { get; }

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public ViewModelBase(INavigationService navigationService,
                             IDialogService dialogService,
                             ILoggingServiceProvider logger,
                             ICommandResolver commandResolver)
        {
            NavigationService = navigationService;
            DialogService = dialogService;
            Logger = logger;
            
            LogOutCommand = commandResolver.AsyncCommand(LogoutCommandHandler);
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

        private Task LogoutCommandHandler()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var token = _cancellationTokenSource.Token;
            token.ThrowIfCancellationRequested();
            
            // var test = await ApiAuthCommand.
            //     ExecuteAsync(_cancellationTokenSource.Token);

            Device.OpenUri(new Uri("https://meetingservice.herokuapp.com/exit"));
            
            return Task.CompletedTask;
        }
    }
}