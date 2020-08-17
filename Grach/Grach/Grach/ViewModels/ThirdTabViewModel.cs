using System;
using System.Windows.Input;
using Grach.Extensions;
using Grach.Pages;
using Grach.ViewModels.Base;
using Prism;
using Prism.Navigation;

using Xamarin.Forms;

namespace Grach.ViewModels
{
    public class ThirdTabViewModel : ViewModelBase, IActiveAware
    {
        private bool isActive;

        public bool IsActive
        {
            get => isActive;
            set
            {
                SetProperty(ref isActive, value);
                this.Log(isActive.ToString());
            }
        }

        public ICommand NavigateToNavigationPageCommand { get; }

        public ThirdTabViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            NavigateToNavigationPageCommand = new Command(NavigateToNavigationPage);
        }

        public event EventHandler IsActiveChanged;

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        private void NavigateToNavigationPage(object obj)
        {
            NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ModalView)}", null, true);
        }
    }
}