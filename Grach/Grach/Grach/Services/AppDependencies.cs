using System.Collections.Generic;
using System.Net.Http;
using Grach.Core;
using Grach.Core.Enums;
using Grach.Core.Helpers;
using Grach.Core.Interfaces;
using Grach.Core.Interfaces.Commanding;
using Grach.Core.Logging;
using Grach.Core.Services;
using Grach.Core.Services.Commanding;
using Grach.Core.Utils.Http.Handlers;
using Grach.Pages;
using Grach.Utills.Constants;
using Grach.ViewModels;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Services.Dialogs;
using Refit;
using Xamarin.Forms;

namespace Grach.Services
{
    public static class AppDependencies
    {
        public static IContainerProvider Container;

        public static void Register(IContainerRegistry containerRegistry, IContainerProvider container)
        {
            Container = container;

            RegisterLoggingServices(containerRegistry);
            RegisterServices(containerRegistry);
            RegisterViewModels(containerRegistry);
            RegisterRefitServices(containerRegistry);
        }
        
        private static void RegisterViewModels(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Xamarin.Forms.NavigationPage>(NavigationKeys.NavigationPageKey);
            containerRegistry.RegisterForNavigation<MeetingsPage, FirstTabViewModel>(NavigationKeys.FirstTabViewKey);
            containerRegistry.RegisterForNavigation<UserInfoPage, SecondTabViewModel>(NavigationKeys.SecondTabViewKey);
            containerRegistry.RegisterForNavigation<MeetingsMapPage, ThirdTabViewModel>(NavigationKeys.ThirdTabViewKey);
            containerRegistry.RegisterForNavigation<NonModalView, NonModalViewModel>(NavigationKeys.NonModalViewKey);
            containerRegistry.RegisterForNavigation<LoginPage, LoginViewModel>(NavigationKeys.ModalViewKey);
            containerRegistry.RegisterForNavigation<MeetingTabbedPage, MainViewModel>(NavigationKeys.MainViewKey);
        }
        
         private static void RegisterServices(IContainerRegistry containerRegistry)
        {
            if (CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.DEBUGMOCK))
            {
                
            }
            else
            {
                containerRegistry.RegisterSingleton<LoggingHttpClientHandler>();
            }
            containerRegistry.RegisterSingleton<ILoggingServiceProvider, LoggingServiceProvider>();
            containerRegistry.RegisterSingleton<ICommandResolver, CommandResolver>();
            containerRegistry.RegisterSingleton<ICommandExecutionLock, CommandExecutionLock>();
            containerRegistry.RegisterSingleton<IExceptionHandler, ExceptionHandlerService>();
            containerRegistry.RegisterSingleton<IDialogService, DialogService>();
        }

        
        private static void RegisterRefitServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance(GetRestServiceFor<ITestApi>());
        }
        
        private static T GetRestServiceFor<T>()
        {
            HttpMessageHandler nativeHandler = Container.Resolve<HttpMessageHandler>();
            var handlers = new List<DelegatingHandler>();

            if (CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.DEBUG))
            {
                handlers.Add(Container.Resolve<LoggingHttpClientHandler>());
            }

            HttpClient defaultHttpClient = DefaultRefitHttpClientHelper.GetHttpClient(nativeHandler, handlers.ToArray());
            return RestService.For<T>(defaultHttpClient);
        }
        
        private static void RegisterLoggingServices(IContainerRegistry containerRegistry)
        {
            if (CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.DEBUG) ||
                CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.DEBUGMOCK))
            {
                containerRegistry.Register<ILoggingService, ConsoleLoggingService>(nameof(ConsoleLoggingService));
            }        
        }
    }
}