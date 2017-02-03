﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Locations;
using App11Athletics.DHCToolkit;
using App11Athletics.Views.Timers;
using Java.Lang.Annotation;
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
        }

        #region Overrides of Page

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            AnimatePages.AnimatePageIn(gridButtons);
            AnimatePages.AnimatePageIn(gridTimer);
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

            var button = (Button)sender;
            if (button != CurrentButton)
            {
                CurrentButton = button;
                gridButtons.RaiseChild(button);
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
            }
            else
            {
                disabled = true;
                await AnimatePages.AnimatePageOut(gridTimer);
                await AnimatePages.AnimatePageOut(gridButtons);
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

        private void AnimateTimerSelect(string styleid, double degrees, double anchor, Grid Lparent)
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
