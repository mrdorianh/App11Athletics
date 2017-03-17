using System;
using System.Threading.Tasks;
using App11Athletics.DHCToolkit;
using App11Athletics.Helpers;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {

            InitializeComponent();
            LabelLoginText = stackLayoutLoginView.Width / 5;
            labelNoConnection.IsVisible = false;
            Opacity = 0.0;
        }

        private void ConnectionCheck()
        {
            Device.StartTimer(TimeSpan.FromMilliseconds(750), OnConnectionCheck);
        }

        private bool OnConnectionCheck()
        {

            if (!CrossConnectivity.Current.IsConnected)
            {
                labelNoConnection.IsVisible = true;
                NotConnected = true;
            }
            else
            {
                labelNoConnection.IsVisible = false;
                NotConnected = false;
            }
            return NotConnected;
        }

        public double LabelLoginText { get; set; }

        #region Overrides of VisualElement


        #endregion

        private void LoginView_OnSizeChanged(object sender, EventArgs e)
        {
            while (Width <= 50)
            {

            }
            LabelLoginText = Width / 12;

        }

        #region Overrides of Page

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            this.FadeTo(1, 300U, Easing.CubicIn);
            await AnimatePages.AnimatePageIn(gridLogin, null);
        }

        #endregion

        public async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            if (disabled)
                return;
            disabled = true;
            var cc = OnConnectionCheck();
            if (cc)
            {
                disabled = false;
                return;
            }
            var p = (Image)sender;
            await Task.WhenAll(p.FadeTo(0, 300), p.ScaleTo(2, 350), label.FadeTo(0, 300));
            LoggedInNavigate(p);
            await Task.Delay(300);
            disabled = false;
        }

        public bool disabled { get; set; }

        public bool NotConnected { get; set; }

        private async void LoggedInNavigate(Image image)
        {

            await DependencyService.Get<IAuthSignIn>().AuthRefresh();
            if (!App.IsUserLoggedIn)
            {
                disabled = false;
                image.Scale = 1;
                image.Opacity = 1;
                label.Opacity = 1;
            }
            else
            {
                await this.FadeTo(0, 350U, Easing.CubicIn);
                Navigation.InsertPageBefore(new HomeMenuView(), this);
                //lets test this later as Reset Main
                await Task.Delay(100);
                //                imageBG.ScaleTo(0, 350U, Easing.CubicOut);
                await AnimatePages.AnimatePageOut(gridLogin, null);
                await Navigation.PopAsync();
                //Navigation.InsertPageBefore(new CarouselPageMenu(), this);
            }

        }
    }
}
