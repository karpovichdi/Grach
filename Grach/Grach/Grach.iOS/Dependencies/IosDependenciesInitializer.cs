using System.Net.Http;
using Grach.Core.Enums;
using Grach.Core.Helpers;
using Grach.Core.Interfaces;
using Grach.iOS.Services;
using Prism;
using Prism.Ioc;
using UserNotifications;

namespace Grach.iOS.Dependencies
{
    public class IosDependenciesInitializer : IPlatformInitializer
    {
        private readonly AppDelegate _appDelegate;

        public IosDependenciesInitializer(AppDelegate appDelegate)
        {
            _appDelegate = appDelegate;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (!CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.DEBUGMOCK))
            {
                containerRegistry.RegisterInstance<HttpMessageHandler>(new NSUrlSessionHandler());
            }

            containerRegistry.RegisterSingleton<IBottomNavigationBarService, IosBottomNavigationBarService>();
        }
    }
}