using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Grach.Extensions;
using Grach.Pages;
using Grach.ViewModels.Base;
using Prism.Navigation;
using Xamarin.Forms;

namespace Grach.ViewModels
{
    public class ModalViewModel : ViewModelBase
    {
        public ICommand NavigateToNextModalCommand { get; }
        public ICommand NavigateBackCommand { get; }

        public ModalViewModel(INavigationService navigationService)
            : base(navigationService) 
        {
            NavigateToNextModalCommand = new Command(NavigateToNextModal);
            NavigateBackCommand = new Command(NavigateBack);
        }

        private void NavigateBack(object obj)
        {
            NavigationService.GoBackAsync();
        }

        private void NavigateToNextModal(object obj)
        {
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ModalView)}", null, true);
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
    }
}