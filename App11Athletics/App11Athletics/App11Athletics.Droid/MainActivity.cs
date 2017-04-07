using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using App11Athletics;
using ImageCircle.Forms.Plugin.Droid;
using Octane.Xam.VideoPlayer.Android;
using XFShapeView.Droid;

namespace App11Athletics.Droid
{
    [Activity(Label = "App11Athletics", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle bundle)
        {
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            ImageCircleRenderer.Init();
            FormsVideoPlayer.Init();

            LoadApplication(new App());
        }
    }
}

