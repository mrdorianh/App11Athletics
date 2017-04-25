using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using App11Athletics.DHCToolkit;
using App11Athletics.Helpers;
using App11Athletics.ViewModels.Timers;
using Xamarin.Forms;

namespace App11Athletics.Views.Timers
{
    public partial class TabataFeatureView : ContentPage, ICleanUp
    {
        public TabataFeatureView()
        {
            InitializeComponent();

            TabataFeatureViewModel = new TabataFeatureViewModel();
            BindingContext = TabataFeatureViewModel;

            if (labelAnimateStatus.Text != null)
                WorkTimePrevious = labelAnimateStatus.Text;
            WorkTimeText = WorkTimePrevious;
            //            tabataOptions.TranslationY = 0;
            //            labelAnimateStatus.Opacity = 0;
            gridTabataPage.Opacity = 0;
            imageBG.Opacity = 0;

        }

        #region Overrides of Page

        protected override bool OnBackButtonPressed()
        {
            if (TabataOptionsUp)
            {
                if (!tabataOptions.Valid)
                    return true;
                gridTabataOptions.TranslateTo(0, Height, 350U, Easing.CubicOut);
                TabataOptionsUp = false;
                return true;
            }
            return base.OnBackButtonPressed();
        }
        protected override async void OnAppearing()
        {
            gridTabataPage.Opacity = 0;
            imageBG.Opacity = 0;
            base.OnAppearing();
            await Task.Delay(100);

            //            imageBG.ScaleTo(1, 350U, Easing.CubicOut);
            imageBG.TranslationX = Width / 2;
            imageBG.TranslationY = -Height;
            imageBG.FadeTo(0.2, 200U, Easing.CubicOut);
            await AnimatePages.BgLogoTask(imageBG, Width / 2, Height / 2);
            await Task.Delay(200);
            gridTabataPage.FadeTo(1, 200U, Easing.CubicOut);
            await AnimatePages.AnimatePageIn(gridTabataPage);

            await Task.Delay(400);
            //            imageBG.ScaleTo(1, 350U, Easing.CubicOut);

        }
        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            //            imageBG.ScaleTo(0, 350U, Easing.CubicOut);
            await AnimatePages.AnimatePageOut(gridTabataPage);
            await Cleanup();
        }
        #endregion

        public TabataFeatureViewModel TabataFeatureViewModel;
        public static bool WorkTimeBool { get; set; }
        public static string WorkTimeText { get; set; } = string.Empty;
        public static string WorkTimePrevious { get; set; } = string.Empty;
        public bool TabataOptionsUp { get; set; }



        private async void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            var label = (Label)sender;
            if (label != null)
            {
                label.AnchorY = 0;

            }
            if (labelAnimateStatus?.Text != null)
                WorkTimeText = labelAnimateStatus.Text;
            if (WorkTimeText == WorkTimePrevious)
                return;

            if (WorkTimeText == "Work Time!")
            {
                statusAnimation.AnimateWorkTimeTask(1);
            }
            else
            {
                statusAnimation.AnimateWorkTimeTask(0);
            }

            if (label != null)
            {
                Debug.WriteLine("animating");
                FadeLabel(label);
            }

            WorkTimePrevious = WorkTimeText;

        }

        private static async Task FadeLabel(Label label)
        {
            label.FadeTo(1, 350U);
            await Task.Delay(2000);
            await label.FadeTo(0, 500U);
        }

        private void LabelAnimateStatus_OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {



        }

        private async void TabataOptions_OnClicked(object sender, EventArgs e)
        {
            TabataFeatureViewModel.TotalRounds = Convert.ToInt32(tabataOptions.TotalRounds);
            TabataFeatureViewModel.TotalRoundTimeTimeSpan = TimeSpan.FromMinutes(tabataOptions.TimeOnMinutes) + TimeSpan.FromSeconds(tabataOptions.TimeOnSeconds);
            TabataFeatureViewModel.TotalTimeOffTimeSpan = TimeSpan.FromMinutes(tabataOptions.TimeOffMinutes) + TimeSpan.FromSeconds(tabataOptions.TimeOffSeconds);
            TabataFeatureViewModel.ElapsedTimeSpan = TabataFeatureViewModel.TotalRoundTimeTimeSpan;
            await gridTabataOptions.TranslateTo(0, Height, 350U, Easing.CubicIn);
            await Task.Delay(100);
            TabataOptionsUp = false;
        }

        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {
            if (TabataOptionsUp || TabataFeatureViewModel.TimerRunning)
                return;
            TabataOptionsUp = true;
            await gridTabataOptions.TranslateTo(0, 0, 350U, Easing.CubicIn);
            TabataFeatureViewModel.ResetCommandMethod();
        }

        #region Implementation of ICleanUp

        public async Task Cleanup()
        {
            await Task.Run(() =>
            {
                TabataFeatureViewModel.ResetCommandMethod();
                TabataFeatureViewModel = null;
                Content = null;
                this.BindingContext = null;
                GC.Collect();
            });
        }

    }

    #endregion
}

