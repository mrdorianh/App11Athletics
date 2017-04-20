
using FFImageLoading.Forms.Touch;
using FFImageLoading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Foundation;
using Octane.Xam.VideoPlayer.iOS;
using Syncfusion.SfCarousel.XForms.iOS;
using UIKit;
using ImageCircle.Forms.Plugin.iOS;
using Plugin.Permissions.Abstractions;
using XFShapeView.iOS;
using XLabs.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services;

namespace App11Athletics.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            global::Xamarin.Forms.Forms.Init();
            CachedImageRenderer.Init();
            new ShapeRenderer();
            ShapeRenderer.Init();
            FormsVideoPlayer.Init();
            new SfCarouselRenderer();
            ImageCircleRenderer.Init();

            if (Resolver.IsSet)
                Resolver.ResetResolver();
            var container = new SimpleContainer();
            container.Register<IDevice>(t => AppleDevice.CurrentDevice);
            container.Register<IDisplay>(t => t.Resolve<IDevice>().Display);
            container.Register<INetwork>(t => t.Resolve<IDevice>().Network);
            container.Register<IDependencyContainer>(t => container);

            Resolver.SetResolver(container.GetResolver());
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
        }


    }
}