using System.Net.Http;
using Grach.Extensions;
using Grach.Pages;
using Grach.ViewModels;
using Prism;
using Prism.Common;
using Prism.DryIoc;
using Prism.Ioc;

using Refit;

using Grach.Extensions;
using Grach.Interfaces.Services.Api;
using Grach.ViewModels;
using Grach.Pages;

using Xamarin.Forms;

using Xamarin.Forms;

namespace Grach
{
    public partial class App
    {
        public static Page CurrentPage
        {
            get => PageUtilities.GetCurrentPage(PrismApplicationBase.Current.MainPage);
        }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            this.Log();
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(MainView)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainView, MainViewModel>();
            containerRegistry.RegisterForNavigation<FirstTabView, FirstTabViewModel>();
            containerRegistry.RegisterForNavigation<SecondTabView, SecondTabViewModel>();
            containerRegistry.RegisterForNavigation<ThirdTabView, ThirdTabViewModel>();
            containerRegistry.RegisterForNavigation<NonModalView, NonModalViewModel>();
            containerRegistry.RegisterForNavigation<ModalView, ModalViewModel>();
        }
    }
}
