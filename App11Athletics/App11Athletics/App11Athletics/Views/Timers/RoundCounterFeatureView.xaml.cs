using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
