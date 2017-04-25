using System.Threading.Tasks;
using App11Athletics.DHCToolkit;
using Xamarin.Forms;

namespace App11Athletics.Views.Timers
{
    public partial class StopwatchFeatureView : ContentPage
    {
        public StopwatchFeatureView()
        {
            InitializeComponent();
            gridStopwatch.Opacity = 0;
            imageBG.Opacity = 0;
        }

        private void ListLapTime_OnItemAppearing(object sender, ItemVisibilityEventArgs e) { }
        protected override async void OnAppearing()
        {
            gridStopwatch.Opacity = 0;
            imageBG.Opacity = 0;
            base.OnAppearing();
            await Task.Delay(100);

            //            imageBG.ScaleTo(1, 350U, Easing.CubicOut);
            imageBG.TranslationX = Width / 2;
            imageBG.TranslationY = -Height;
            imageBG.FadeTo(0.5, 200U, Easing.CubicOut);
            AnimatePages.BgLogoTask(imageBG, Width / 2, Height / 2);
            await Task.Delay(1000);
            gridStopwatch.FadeTo(1, 200U, Easing.CubicOut);
            await AnimatePages.AnimatePageIn(gridStopwatch);

            await Task.Delay(400);
            //            imageBG.ScaleTo(1, 350U, Easing.CubicOut);


        }
        protected override async void OnDisappearing()
        {
            base.OnAppearing();
            //            imageBG.ScaleTo(0, 350U, Easing.CubicOut);
            await AnimatePages.AnimatePageOut(gridStopwatch);
        }
    }
}
