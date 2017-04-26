using System;
using System.ComponentModel;
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
            Opacity = 0;
        }

        #region Overrides of Page

        protected override async void OnAppearing()
        {
            Opacity = 0;
            base.OnAppearing();
            disabled = true;
            CurrentButton = null;
            await Task.WhenAny(AnimatePages.AnimatePageIn(stackLayout), Task.Delay(50));
            await this.FadeTo(1);
            disabled = false;
        }

        #endregion

        public Button CurrentButton { get; set; }

        public double ButtonSizeX => Width / 2.5;

        public double ButtonSizeY => ButtonSizeX * 0.75;


        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (disabled)
                return;
            disabled = true;
            var button = (Button)sender;
            stackLayout.RaiseChild(button);
            await AnimateTimerSelect(stackLayout, button.StyleId);



            //                                if (disabled)
            //                                    return;
            await Task.Delay(50);
            await AnimatePages.AnimatePageOut(stackLayout);
            switch (button.StyleId)
            {
                case "t":

                    await Navigation.PushAsync(new TabataFeatureView());
                    break;
                case "r":
                    await Navigation.PushAsync(new RoundCounterFeatureView());
                    break;
                case "s":
                    await Navigation.PushAsync(new StopwatchFeatureView());
                    break;
            }
            await AnimateTimerSelect(stackLayout);
            disabled = false;


        }

        public Task<bool> AnimateTimerSelect(StackLayout Lparent, string styleid = null)
        {

            foreach (var view in Lparent.Children)
            {
                if (view.StyleId != styleid)
                    continue;
                view.ScaleTo(1.2, 250U, Easing.SpringOut);
                view.BackgroundColor = Color.FromHex("#029902");
            }
            foreach (var view in Lparent.Children)
            {
                if (view.StyleId == styleid)
                    continue;
                view.ScaleTo(1.0, 250U, Easing.CubicIn);
                view.BackgroundColor = Color.FromHex("#005EBF");
            }
            return Task.FromResult(true);

        }

        public void TimerMenu_OnSizeChanged(object sender, EventArgs e)
        {
            foreach (var button in stackLayout.Children)
            {
                button.HeightRequest = ButtonSizeY;
                button.WidthRequest = ButtonSizeX;
            }
        }


    }
}
