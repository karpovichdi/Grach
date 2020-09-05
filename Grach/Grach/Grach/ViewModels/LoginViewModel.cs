using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Core.Interfaces;
using Grach.Core.Interfaces.Commanding;
using Grach.Extensions;
using Grach.Pages;
using Grach.Services;
using Grach.Utills.Constants;
using Grach.ViewModels.Base;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace Grach.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private CancellationTokenSource _cancellationTokenSource;

        public ICommand NavigateToNextModalCommand { get; }
        public ICommand NavigateBackCommand { get; }
        public ICommand UnauthorizedInputCommand { get; set; }
        public ICommand LoginViaGoogleCommand { get; }
        
        private IAsyncResultCommand<CancellationToken, string> ApiAuthCommand { get; }

        public LoginViewModel(ITestApi testApi,
                              ICommandResolver commandResolver,
                              INavigationService navigationService,
                              IDialogService dialogService,
                              ILoggingServiceProvider logger)
            : base(navigationService, dialogService, logger, commandResolver)
        {
            NavigateToNextModalCommand = new Command(NavigateToNextModal);
            NavigateBackCommand = new Command(NavigateBack);
            LoginViaGoogleCommand = commandResolver.AsyncCommand(LoginViaGoogleCommandHandler, true);
            UnauthorizedInputCommand = new Command(UnauthorizedInputCommandHandler);
            
            ApiAuthCommand = commandResolver.AsyncResultCommand<CancellationToken, string>(
                execute: parameters => testApi.Authorize(parameters), 
                ignoreLock: true);
        }

        public override void OnAppearing()
        {
            TestApi();
        }

        private async Task TestApi()
        {
            
        }

        private void NavigateBack(object obj)
        {
            NavigationService.GoBackAsync();
        }

        private void NavigateToNextModal(object obj)
        {
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LoginView)}", null, true);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            this.Log(parameters.GetValue<string>("param"));
            base.Initialize(parameters);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            this.Log(parameters.GetValue<string>("param"));
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.Log(parameters.GetValue<string>("param"));
            base.OnNavigatedTo(parameters);
        }

        private async Task LoginViaGoogleCommandHandler()
        {
            _cancellationTokenSource = new CancellationTokenSource();

            var token = _cancellationTokenSource.Token;
            token.ThrowIfCancellationRequested();
            
            // var test = await ApiAuthCommand.
            //     ExecuteAsync(_cancellationTokenSource.Token);

            Device.OpenUri(new Uri("https://meetingservice.herokuapp.com/showuser"));
        }
        
        private void UnauthorizedInputCommandHandler(object o)
        {
            NavigationService.NavigateAsync($"/{NavigationKeys.NavigationPageKey}/{NavigationKeys.MainViewKey}");
        }
    }
}