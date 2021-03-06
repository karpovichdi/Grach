﻿using Grach.Core.Enums;
using Grach.Core.Helpers;
using Grach.Core.Interfaces;
using Grach.Extensions;
using Grach.Pages;
using Grach.Services;
using Grach.ViewModels;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
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

        protected override void OnStart()
        {
            if (CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.RELEASE2))
            { 
                AppCenter.Start("android=00a649e6-a0a2-4f4d-b759-b967f0da9778;" +
                                "ios=bc8dc21e-37e1-435c-ac44-128eddda000c;",
                    typeof(Analytics), typeof(Crashes));
            }
            
            if (CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.RELEASE))
            { 
                AppCenter.Start("android=8eb6a825-d938-4901-a56f-fcb6cd695d3f;" +
                                "ios=43bc1e3a-03c6-4289-ab08-da069db424fd;",
                    typeof(Analytics), typeof(Crashes));
            }
        }
    }
}