using Xamarin.Essentials;

namespace Grach.Extensions
{
    public static class NetworkAccessExtensions
    {
        public static bool IsConnected(this NetworkAccess networkAccess)
        {
            return networkAccess == NetworkAccess.Internet;
        }
    }
}