using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Media;
using Octane.Xam.VideoPlayer;
using Xamarin.Forms;


namespace App11Athletics.Views
{
    public partial class Splash : ContentPage
    {
        public Splash()
        {
            InitializeComponent();
            videoPlayer.Source = Octane.Xam.VideoPlayer.VideoSource.FromResource("video.mp4");
        }

        #region Overrides of Page

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }

        #endregion
    }
}
