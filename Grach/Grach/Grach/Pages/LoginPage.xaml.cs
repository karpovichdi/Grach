using Xamarin.Forms;
using Grach.Core.Enums;
using Grach.Core.Helpers;

using Xamarin.Forms.Xaml;

namespace Grach.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            
            if (CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.RELEASE))
            {
                contentGrid.BackgroundColor = Color.Blue;
            }

            if (CompilerFlagHelper.CompilerDirectives.HasFlag(CompilerDirectives.RELEASE2))
            {
                contentGrid.BackgroundColor = Color.Red;
            }
        }
    }
}