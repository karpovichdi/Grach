using System.Net.Http;
using Prism;
using Prism.Ioc;
using Xamarin.Android.Net;

namespace Grach.Droid.Initializer
{
    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<HttpMessageHandler>(new AndroidClientHandler());
        }
    }
}