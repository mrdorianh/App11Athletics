using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using App11Athletics;
using FFImageLoading;
using FFImageLoading.Forms.Droid;
using ImageCircle.Forms.Plugin.Droid;
using Octane.Xam.VideoPlayer.Android;
using Syncfusion.SfCarousel.XForms;
using Xamarin.Forms.Platform.Android;
using XFShapeView.Droid;
using XLabs.Forms;
using XLabs.Ioc;

//using Xamarin.Forms.Platform.Android;
using XLabs.Platform.Device;
using XLabs.Platform.Services;

namespace App11Athletics.Droid
{
    [Activity(Label = "App11Athletics", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : XFormsAppCompatDroid
    {

        protected override void OnCreate(Bundle bundle)
        {
            Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            CachedImageRenderer.Init();
          ImageCircle.Forms.Plugin.Droid.ImageCircleRenderer.Init();
            FormsVideoPlayer.Init();

            //            Resolver.ResetResolver();
            if (Resolver.IsSet)
                Resolver.ResetResolver();
            var container = new SimpleContainer();
            container.Register<IDevice>(t => AndroidDevice.CurrentDevice);
            container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
            container.Register<INetwork>(t => t.Resolve<IDevice>().Network);
            Resolver.SetResolver(container.GetResolver());
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

//global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity