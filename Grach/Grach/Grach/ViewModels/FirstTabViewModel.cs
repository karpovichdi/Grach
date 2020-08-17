using System;
using System.Collections.Generic;
using System.Windows.Input;
using Grach.Extensions;
using Grach.Pages;
using Grach.ViewModels.Base;
using Prism;
using Prism.Navigation;

using Xamarin.Forms;

namespace Grach.ViewModels
{
    public class FirstTabViewModel : ViewModelBase, IActiveAware
    {
        private bool isActive;

        public IList<int> Indexes { get; }

        public event EventHandler IsActiveChanged;

        public ICommand NavigateToNavigationPageCommand { get; }

        public bool IsActive
        {
            get => isActive;
            set
            {
                SetProperty(ref isActive, value);
                this.Log(isActive.ToString());
            }
        }

        public FirstTabViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Indexes = new List<int>(49);
            for (int i = 0; i < 49; i++)
                Indexes.Add(i);

            NavigateToNavigationPageCommand = new Command(NavigateToNavigationPage);
        }

        public override void Initialize(INavigationParameters parameters)
        {
            base.Initialize(parameters);
        }

        private void NavigateToNavigationPage(object obj)
        {
            this.Log();
            NavigationService.NavigateAsync(nameof(NonModalView));
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            this.Log();
            base.OnNavigatedFrom(parameters);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.Log();
            base.OnNavigatedTo(parameters);
        }
    }
}