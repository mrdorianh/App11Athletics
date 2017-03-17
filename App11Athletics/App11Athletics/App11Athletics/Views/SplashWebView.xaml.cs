using System.Threading.Tasks;
using App11Athletics.Helpers;
using Octane.Xam.VideoPlayer.Events;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11Athletics.Views
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashWebView : ContentPage
    {
        public SplashWebView()
        {
            InitializeComponent();
            videoPlayer.Opacity = 0;
            var c = Plugin.Connectivity.CrossConnectivity.Current.IsConnected;
            if (!c)
                VideoPlayer_OnCompleted(null, null);

            //            var htmlSource = new HtmlWebViewSource();
            //            htmlSource.Html = @"<iframe src='https://player.vimeo.com/video/206658139?autoplay=1&title=0&byline=0&portrait=0' width='100%' height='100%' frameborder='0' ></iframe>";
            //
            //            webView.Source = htmlSource;

        }

        private void VideoPlayer_OnFailed(object sender, Octane.Xam.VideoPlayer.Events.VideoPlayerErrorEventArgs e)
        {
            VideoPlayer_OnCompleted(null, null);
        }

        private async void VideoPlayer_OnCompleted(object sender, Octane.Xam.VideoPlayer.Events.VideoPlayerEventArgs e)
        {
            this.FadeTo(0, 300U, Easing.CubicIn);
            if (string.IsNullOrEmpty(Settings.UserRefreshToken))
            {
                App.IsUserLoggedIn = false;
                Navigation.InsertPageBefore(new LoginView(), this);
            }
            else
            {
                App.IsUserLoggedIn = true;
                //                DependencyService.Get<IAuthSignIn>().AuthRefresh();
                Navigation.InsertPageBefore(new HomeMenuView(), this);
            }
            await Task.Delay(100);
            await Navigation.PopAsync(true);
            videoPlayer = null;
            Content = null;
        }

        private void VideoPlayer_OnPlaying(object sender, VideoPlayerEventArgs e)
        {
            videoPlayer.FadeTo(1, 300U, Easing.CubicIn);
        }
    }

}
