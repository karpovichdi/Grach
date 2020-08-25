using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Core.Interfaces;
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
        public ICommand NavigateToNextModalCommand { get; }
        public ICommand NavigateBackCommand { get; }
        
        public ICommand LoginViaGoogleCommand { get; }
        
        public LoginViewModel(ICommandResolver commandResolver,
                              INavigationService navigationService,
                              IDialogService dialogService,
                              ILoggingServiceProvider logger)
            : base(navigationService, dialogService, logger)
        {
            NavigateToNextModalCommand = new Command(NavigateToNextModal);
            NavigateBackCommand = new Command(NavigateBack);
            
            LoginViaGoogleCommand = commandResolver.AsyncCommand(LoginViaGoogleCommandHandler, true);
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

        private Task LoginViaGoogleCommandHandler()
        {
            return NavigationService.NavigateAsync($"/{NavigationKeys.NavigationPageKey}/{NavigationKeys.MainViewKey}");
        }
    }
}