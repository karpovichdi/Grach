using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Core.Interfaces;
using Grach.Extensions;
using Grach.Pages;
using Grach.ViewModels.Base;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace Grach.ViewModels
{
    public class NonModalViewModel : ViewModelBase, IConfirmNavigation
    {
        public string Value => $"This is New Navigation Page {DateTime.Now.Second}";

        public ICommand NavigateToNavigationPageCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public NonModalViewModel(ICommandResolver commandResolver,
               INavigationService navigationService,
               IDialogService dialogService,
               ILoggingServiceProvider logger)
            : base(navigationService, dialogService, logger, commandResolver) 
        {
            NavigateToNavigationPageCommand = new Command(NavigateToNavigationPage);
            NavigateBackCommand = new Command(NavigateBack);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            this.Log(parameters.GetValue<string>("param"));
            base.Initialize(parameters);
        }

        private void NavigateBack(object obj)
        {
            NavigationService.GoBackAsync();
        }


        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
        }

        public bool CanNavigate(INavigationParameters parameters)
        {
            this.Log();
            return true;
        }

        private void NavigateToNavigationPage(object obj)
        {
            NavigationService.NavigateAsync(nameof(NonModalView), new NavigationParameters("param=forward"));
        }
    }
}