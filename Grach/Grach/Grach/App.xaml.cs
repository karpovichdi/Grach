using Grach.Core.Interfaces;
using Grach.Extensions;
using Grach.Pages;
using Grach.Services;
using Grach.ViewModels;
using Prism;
using Prism.Common;
using Prism.Ioc;
using Xamarin.Forms;

namespace Grach
{
    public partial class App
    {
        private ILoggingServiceProvider _logger;

        public static Page CurrentPage
        {
            get => PageUtilities.GetCurrentPage(PrismApplicationBase.Current.MainPage);
        }

        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            _logger = Container.Resolve<ILoggingServiceProvider>();
            
            this.Log();
            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(LoginPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            AppDependencies.Register(containerRegistry, Container);
        }
    }
}
