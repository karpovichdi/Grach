using Grach.Pages;
using Xamarin.Forms;

namespace Grach.Utills.Constants
{
    public static class NavigationKeys
    {
        public const string NavigationPageKey = nameof(NavigationPage);
        public static string FirstTabViewKey = nameof(MeetingsPage);
        public static string ModalViewKey = nameof(LoginPage);
        public static string NonModalViewKey = nameof(NonModalView);
        public static string ThirdTabViewKey = nameof(MeetingsMapPage);
        public static string SecondTabViewKey = nameof(UserInfoPage);
        public static string MainViewKey = nameof(MeetingTabbedPage);
    }
}