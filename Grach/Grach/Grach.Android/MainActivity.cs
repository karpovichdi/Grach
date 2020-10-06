using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Grach.Core.Enums;
using Grach.Core.Helpers;
using Grach.Droid.Dependencies;
using Prism.Common;
using Grach.Droid.Initializer;
using Grach.Extensions;
using Grach.Interfaces;

using Xamarin.Forms;

namespace Grach.Droid
{
    [Activity(Label = "Grach", 
              Icon = "@mipmap/icon", 
              Theme = "@style/MainTheme", 
              MainLauncher = true, 
              LaunchMode = LaunchMode.SingleInstance,
              ScreenOrientation =  ScreenOrientation.Portrait,
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const int BackButtonId = 16908332;
        
        public static MainActivity Instance { get; private set; }
        
        public MainActivity()
        {
            Instance = this;
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            SetResources();
            base.OnCreate(savedInstanceState);
            InitializeApplication(savedInstanceState);
        }

        private void InitializeApplication(Bundle savedInstanceState)
        {
            SetCompilerDirectives();

            // if (!this.IsPackageInstalled(ConstantsDroid.PackageNames.Chrome))
            //     IsEmbeddedWebView = true;

            //Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState);
            
            LoadApplication(new App(new DroidDependenciesInitializer(this)));
        }

        private void SetCompilerDirectives()
        {
#if DEBUG
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.DEBUG;
#endif

#if DEBUGMOCK
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.DEBUGMOCK;
#endif

#if RELEASE
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.RELEASE;
#endif

#if ENABLE_APPCENTERLOGGER
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.ENABLE_APPCENTERLOGGER;
#endif

#if TEST
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.TEST;
#endif
            
#if RELEASE2
            CompilerFlagHelper.CompilerDirectives |= CompilerDirectives.RELEASE2;
#endif
        }

        private void SetResources()
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            // transparency for status bar
            Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);

            base.SetTheme(Resource.Style.MainTheme);
        }
        
        private void SetToolBar()
        {
            Android.Support.V7.Widget.Toolbar toolbar = this.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

            SetSupportActionBar(toolbar);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == BackButtonId &&
                App.CurrentPage.BindingContext is IBackNavigationHandler backNavigationHandler)
            {
                backNavigationHandler.NavigateBack();
                return false;
            }

            return base.OnOptionsItemSelected(item);
        }

        public override void OnBackPressed()
        {
            if (App.CurrentPage.BindingContext is IBackNavigationHandler backNavigationHandler)
            {
                backNavigationHandler.NavigateBack(null);
                return;
            }

            base.OnBackPressed();
        }
    }
}