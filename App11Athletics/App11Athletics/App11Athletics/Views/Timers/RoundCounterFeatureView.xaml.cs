using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.DHCToolkit;
using App11Athletics.ViewModels.Timers;

using Xamarin.Forms;

namespace App11Athletics.Views.Timers
{
    public partial class RoundCounterFeatureView : ContentPage
    {
        public RoundCounterFeatureView()
        {
            InitializeComponent();
            RoundCounterFeatureViewModel = new RoundCounterFeatureViewModel();
            BindingContext = RoundCounterFeatureViewModel;
            RoundOptions.TranslationY = 0;

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
            imageBG.FadeTo(0.5, 200U, Easing.CubicOut);
            AnimatePages.BgLogoTask(imageBG, Width / 2, Height / 2);
            await Task.Delay(1000);
            gridRoundCounterPage.FadeTo(1, 200U, Easing.CubicOut);
            await AnimatePages.AnimatePageIn(gridRoundCounterPage, imageBG);

            await Task.Delay(400);
        }
        protected override async void OnDisappearing()
        {
            base.OnAppearing();
            //            imageBG.ScaleTo(0, 350U, Easing.CubicOut);
            await AnimatePages.AnimatePageOut(gridRoundCounterPage, imageBG);
        }
        public RoundCounterFeatureViewModel RoundCounterFeatureViewModel;
        public bool RoundOptionsUp { get; set; }
        private void RoundOptions_OnClicked(object sender, EventArgs e)
        {
            RoundCounterFeatureViewModel.TotalRounds = Convert.ToInt32(RoundOptions.TotalRounds);
            RoundCounterFeatureViewModel.TotalRoundTimeTimeSpan = TimeSpan.FromMinutes(RoundOptions.TimeOnMinutes) + TimeSpan.FromSeconds(RoundOptions.TimeOnSeconds);
            RoundCounterFeatureViewModel.TotalRounds = Convert.ToInt32(RoundOptions.TotalRounds);

            RoundOptions.TranslateTo(0, Height, 350U, Easing.CubicIn);
            RoundOptionsUp = false;
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            if (RoundOptionsUp || RoundCounterFeatureViewModel.TimerRunning)
                return;
            RoundOptionsUp = true;
            await RoundOptions.TranslateTo(0, 0, 350U, Easing.CubicIn);
            RoundCounterFeatureViewModel.ResetCommandMethod();
        }
        protected override bool OnBackButtonPressed()
        {
            if (RoundOptionsUp)
            {
                if (!RoundOptions.Valid)
                    return true;
                RoundOptions.TranslateTo(0, Height, 350U, Easing.CubicOut);
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
    }
}
