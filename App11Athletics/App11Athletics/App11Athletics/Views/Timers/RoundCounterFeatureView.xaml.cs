using System;
using System.ComponentModel;
using App11Athletics.Helpers;
using System.Threading.Tasks;
using App11Athletics.DHCToolkit;
using App11Athletics.ViewModels.Timers;
using Xamarin.Forms;

namespace App11Athletics.Views.Timers
{
    public partial class RoundCounterFeatureView : ContentPage, ICleanUp
    {
        public RoundCounterFeatureView()
        {
            InitializeComponent();
            RoundCounterFeatureViewModel = new RoundCounterFeatureViewModel();
            BindingContext = RoundCounterFeatureViewModel;
            //            gridTabataOptions.TranslationY = 0;
            gridRoundCounterPage.Opacity = 0;
            imageBG.Opacity = 0;
        }
        protected override async void OnAppearing()
        {
            gridRoundCounterPage.Opacity = 0;
            imageBG.Opacity = 0;
            base.OnAppearing();
            await Task.Delay(100);
            //            imageBG.ScaleTo(1, 350U, Easing.CubicOut);
            imageBG.TranslationX = Width / 2;
            imageBG.TranslationY = -Height;
            imageBG.FadeTo(0.2, 200U, Easing.CubicOut);
            await AnimatePages.BgLogoTask(imageBG, Width / 2, Height / 2);
            await Task.Delay(200);
            gridRoundCounterPage.FadeTo(1, 200U, Easing.CubicOut);
            await AnimatePages.AnimatePageIn(gridRoundCounterPage, null);

            await Task.Delay(400);
        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            //            imageBG.ScaleTo(0, 350U, Easing.CubicOut);
            await AnimatePages.AnimatePageOut(gridRoundCounterPage, null);
            await Cleanup();
        }
        public RoundCounterFeatureViewModel RoundCounterFeatureViewModel;

        public bool RoundOptionsUp { get; set; }
        private async void RoundOptions_OnClicked(object sender, EventArgs e)
        {
            RoundCounterFeatureViewModel.TotalRounds = Convert.ToInt32(RoundOptions.TotalRounds);
            RoundCounterFeatureViewModel.TotalRoundTimeTimeSpan = TimeSpan.FromMinutes(RoundOptions.TimeOnMinutes) + TimeSpan.FromSeconds(RoundOptions.TimeOnSeconds);
            RoundCounterFeatureViewModel.TotalRounds = Convert.ToInt32(RoundOptions.TotalRounds);
            RoundCounterFeatureViewModel.ElapsedTimeSpan = RoundCounterFeatureViewModel.TotalRoundTimeTimeSpan;
            await gridTabataOptions.TranslateTo(0, Height, 300U, Easing.CubicIn);
            await Task.Delay(100);
            RoundOptionsUp = false;
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            if (RoundOptionsUp || RoundCounterFeatureViewModel.TimerRunning)
                return;
            RoundOptionsUp = true;
            await gridTabataOptions.TranslateTo(0, 0, 350U, Easing.CubicIn);
            await Task.Delay(200);
            RoundCounterFeatureViewModel.ResetCommandMethod();
        }
        protected override bool OnBackButtonPressed()
        {
            if (RoundOptionsUp)
            {
                if (!RoundOptions.Valid)
                    return true;
                gridTabataOptions.TranslateTo(0, Height, 350U, Easing.CubicOut);
                RoundOptionsUp = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }

        private void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (RoundCounterFeatureViewModel != null && RoundCounterFeatureViewModel.TimerRunning)
                statusAnimation.AnimateWorkTimeTask(1);
        }

        #region Implementation of ICleanUp

        public async Task Cleanup()
        {
            await Task.Run(() =>
            {
                RoundCounterFeatureViewModel.ResetCommandMethod();
                RoundCounterFeatureViewModel = null;
                Content = null;
                this.BindingContext = null;
                GC.Collect();
            });
        }

        #endregion
    }
}
