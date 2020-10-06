using Grach.Core.Interfaces;
using Xamarin.Forms.Platform.Android;

namespace Grach.Droid.Services
{ 
   public class DroidBottomNavigationBarService : IBottomNavigationBarService
   {
       public double GetHeight()
       {
           var height = MainActivity.Instance.Window?.DecorView?.RootWindowInsets?.StableInsetBottom;

           return MainActivity.Instance.FromPixels(height ?? 0);
       }
   }
}