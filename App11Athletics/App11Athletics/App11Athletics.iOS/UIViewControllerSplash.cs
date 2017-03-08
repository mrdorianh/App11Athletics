using System;
using System.Drawing;
using App11Athletics.iOS;
using App11Athletics;
using App11Athletics.Views;
using CoreFoundation;
using CoreGraphics;
using UIKit;
using Foundation;
using MediaPlayer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


//[assembly: ExportRenderer(typeof(SplashWebView), typeof(UIViewControllerSplash))]
namespace App11Athletics.iOS
{
    [Register("UIViewControllerSplash")]
    public class UIViewControllerSplash : PageRenderer
    {
        MPMoviePlayerController moviePlayer;
        private UIView mainviewUiView;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
            {
                return;
            }

            try
            {
                SetupUserInterface();
                moviePlayer.Play();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(@"			ERROR: ", ex.Message);
            }
        }

        private void SetupUserInterface()
        {

            mainviewUiView = new UIView() { Frame = new CGRect(0f, 0f, 320f, View.Bounds.Height) };
            var urltoplay = new NSUrl("https://player.vimeo.com/video/206658139?autoplay=1&title=0&byline=0&portrait=0");
            moviePlayer = new MPMoviePlayerController();
            // set the URL to the Video Player
            moviePlayer.ContentUrl = urltoplay;
            moviePlayer.View.Frame = new CGRect(55, 170, 310f, 200f);
            // Set this property True if you want the video to be auto played on page load
            moviePlayer.ShouldAutoplay = true;
            // If you want to keep the Video player on-ready-to-play state, then enable this
            // This will keep the video content loaded from the URL, untill you play it.
            moviePlayer.PrepareToPlay();
            // Enable the embeded video controls of the Video Player, this has several types of Embedded controls for you to choose
            moviePlayer.ControlStyle = MPMovieControlStyle.Embedded;

            //            View.Add(mainviewUiView);
            View.Add(moviePlayer.View);

        }
    }
}