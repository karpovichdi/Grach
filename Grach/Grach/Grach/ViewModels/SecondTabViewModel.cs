using System;
using System.Windows.Input;
using Grach.Core.Interfaces;
using Grach.Extensions;
using Grach.Pages;
using Grach.ViewModels.Base;
using Prism;
using Prism.Navigation;
using Prism.Navigation.TabbedPages;
using Prism.Services.Dialogs;
using Xamarin.Forms;

namespace Grach.ViewModels
{
    public class SecondTabViewModel : ViewModelBase, IActiveAware
    {
        private bool isActive;

        public event EventHandler IsActiveChanged;

        public ICommand NavigateToNavigationPageCommand { get; }
        public ICommand SwitchTabCommand { get; }

        public bool IsActive 
        { 
            get => isActive;
            set
            {
                SetProperty(ref isActive, value);
                this.Log(isActive.ToString());
            }
        }

        public SecondTabViewModel(ICommandResolver commandResolver,
                                  INavigationService navigationService,
                                  IDialogService dialogService,
                                  ILoggingServiceProvider logger)
            : base(navigationService, dialogService, logger)
        {
            NavigateToNavigationPageCommand = new Command(NavigateToNavigationPage);
            SwitchTabCommand = new Command(SwitchTab);
        }

        private void SwitchTab(object obj)
        {
            NavigationService.SelectTabAsync(nameof(ThirdTabView));
        }

        private void NavigateToNavigationPage(object obj)
        {
            NavigationService.NavigateAsync(nameof(NonModalView), new NavigationParameters("param=param_second"));
        }

        public override void Initialize(INavigationParameters parameters)
        {
            this.Log();
            base.Initialize(parameters);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.Log();
            base.OnNavigatedTo(parameters);
        }
    }
}