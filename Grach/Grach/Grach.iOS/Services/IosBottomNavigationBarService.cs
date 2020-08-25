using Grach.Core.Interfaces;
using UIKit;

namespace Grach.iOS.Services
{
    public class IosBottomNavigationBarService : IBottomNavigationBarService
    {
        public double GetHeight()
        {
            var safeArea = UIApplication.SharedApplication?.KeyWindow?.SafeAreaInsets ?? default;
            return safeArea.Bottom;
        }
    }
}