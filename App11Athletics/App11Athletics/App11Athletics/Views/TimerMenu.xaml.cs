using System;
using System.Threading.Tasks;
using App11Athletics.DHCToolkit;
using App11Athletics.Views.Timers;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class TimerMenu : ContentPage
    {
        public bool disabled;

        public TimerMenu()
        {
            InitializeComponent();
            TimerSize = grid.Width / 4;
            imagetimer.WidthRequest = TimerSize;
            grid.Opacity = 0;
        }

        #region Overrides of Page

        protected override async void OnAppearing()
        {
            grid.Opacity = 0;
            base.OnAppearing();
            disabled = true;
            CurrentButton = null;
            AnimatePages.AnimatePageIn(gridTimer, null);
            grid.FadeTo(1, 200U, Easing.CubicIn);
            await AnimatePages.AnimatePageIn(gridButtons, null);
            await Task.Delay(100);
            disabled = false;
        }

        #endregion

        public Button CurrentButton { get; set; }
        public double TimerSize { get; set; }
        public double ButtonSizeX { get; set; }
        public double ButtonSizeY { get; set; }


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (disabled)
                return;
            disabled = false;
            var button = (Button)sender;
            if (button != CurrentButton)
            {
                CurrentButton = button;
                gridButtons.RaiseChild(button);
                disabled = true;
                switch (button.StyleId)
                {
                    case "t":

                        AnimateTimerSelect(button.StyleId, -25, 0, gridButtons);

                        break;
                    case "r":
                        AnimateTimerSelect(button.StyleId, 0, .5, gridButtons);

                        break;
                    case "s":
                        AnimateTimerSelect(button.StyleId, 25, 1, gridButtons);

                        break;
                }
                //                await Task.Delay(350);

            }
            else
            {
                //                                if (disabled)
                //                                    return;
                disabled = true;
                await imagetimer.RotateTo(180, 350U, Easing.CubicOut);
                await Task.Delay(100);
                await AnimatePages.AnimatePageOut(gridTimer, null);
                await AnimatePages.AnimatePageOut(gridButtons, null);
                switch (button.StyleId)
                {
                    case "t":

                        await Navigation.PushAsync(new TabataFeatureView());
                        disabled = false;
                        TapGestureRecognizerResetSelection_OnTapped(null, null);
                        break;
                    case "r":
                        await Navigation.PushAsync(new RoundCounterFeatureView());
                        disabled = false;
                        TapGestureRecognizerResetSelection_OnTapped(null, null);
                        break;
                    case "s":
                        await Navigation.PushAsync(new StopwatchFeatureView());
                        disabled = false;
                        TapGestureRecognizerResetSelection_OnTapped(null, null);
                        break;
                }

            }

        }

        public void AnimateTimerSelect(string styleid, double degrees, double anchor, Grid Lparent)
        {
            foreach (var view in Lparent.Children)
            {

                if (view.GetType() != typeof(Button))
                    return;
                if (view.StyleId != styleid)
                {
                    view.FadeTo(0.4, 300U, Easing.CubicOut);
                    view.ScaleTo(1.0, 250U, Easing.CubicOut);
                    view.AnchorX = 0.5;
                    view.BackgroundColor = Color.FromHex("#005EBF");
                }
                else
                {

                    imagetimer.RotateTo(degrees, 350U, Easing.CubicOut);
                    view.FadeTo(1, 300U, Easing.CubicOut);
                    view.ScaleTo(1.2, 250U, Easing.CubicOut);
                    view.AnchorX = anchor;
                    view.BackgroundColor = Color.FromHex("#029902");

                }
            }
            disabled = false;
        }

        public void TimerMenu_OnSizeChanged(object sender, EventArgs e)
        {
            TimerSize = grid.Width / 4;
            ButtonSizeX = gridButtons.Width / 3;
            ButtonSizeY = gridButtons.Height / 2;
            imagetimer.WidthRequest = TimerSize;

        }

        private void TapGestureRecognizerResetSelection_OnTapped(object sender, EventArgs e)
        {
            if (disabled)
                return;
            CurrentButton = null;
            imagetimer.RotateTo(0, 350U, Easing.CubicOut);
            foreach (var view in gridButtons.Children)
            {
                view.FadeTo(1, 300U, Easing.CubicOut);
                view.ScaleTo(1, 250U, Easing.CubicOut);
                view.AnchorX = 0.5;
                view.BackgroundColor = Color.FromHex("#005EBF");
            }

        }
    }
}
