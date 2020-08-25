using System;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Prism.Common;
using Grach.Droid.Initializer;
using Grach.Extensions;
using Grach.Interfaces;

using Xamarin.Forms;

namespace Grach.Droid
{
    [Activity(Label = "Grach", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public const int BackButtonId = 16908332;
        public static Context Context { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Context = this;

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.Init(this, savedInstanceState); 

            LoadApplication(new App(new AndroidInitializer()));
            SetToolBar();
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