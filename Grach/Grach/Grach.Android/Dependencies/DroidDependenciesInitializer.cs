using System.Net.Http;
using Grach.Core.Enums;
using Grach.Core.Helpers;
using Grach.Core.Interfaces;
using Grach.Droid.Services;
using Prism;
using Prism.Ioc;
using Xamarin.Android.Net;

namespace Grach.Droid.Dependencies
{
    public class DroidDependenciesInitializer : IPlatformInitializer
    {
        private readonly MainActivity _activity;

        public DroidDependenciesInitializer(MainActivity activity)
        {
            _activity = activity;
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            if (!CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.DEBUGMOCK))
            {
                containerRegistry.RegisterInstance<HttpMessageHandler>(new AndroidClientHandler());
            }
            containerRegistry.RegisterSingleton<IBottomNavigationBarService, DroidBottomNavigationBarService>();
        }
    }
}