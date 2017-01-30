using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Helpers;
using App11Athletics.ViewModels;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {

            InitializeComponent();

        }

        public double LabelLoginText { get; set; }

        #region Overrides of VisualElement


        #endregion

        private void LoginView_OnSizeChanged(object sender, EventArgs e)
        {

            LabelLoginText = stackLayoutLoginView.Width / 5;
        }

        public async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var p = (Image)sender;
            await Task.WhenAll(p.FadeTo(0, 300), p.ScaleTo(2, 350), label.FadeTo(0, 300), label.ScaleTo(2, 350));
            LoggedInNavigate();
            //            Application.Current.MainPage = new NavigationPage(new Discover11AthleticsView());
            //            Navigation.InsertPageBefore(new Discover11AthleticsView(), this);
            //            await Navigation.PopAsync();
            //            //            if (!App.IsUserLoggedIn)
            //            //                return;
            //            //            //            await Navigation.PushModalAsync(new NavigationPage(new AuthUserSignIn()));
            //            //            if (App.IsUserLoggedIn)
            //            //            {
            //            //                Navigation.InsertPageBefore(new HomeMenuView(), this);
            //            //                await Navigation.PopAsync();
            //
            //            //            }
        }

        private async void LoggedInNavigate()
        {
            await DependencyService.Get<IAuthSignIn>().AuthRefresh();
            if (!App.IsUserLoggedIn)
            {
                Navigation.InsertPageBefore(new HomeMenuView(), this);
            }
            else
            {
                Navigation.InsertPageBefore(new HomeMenuView(), this);
                //lets test this later as Reset Main

                //Navigation.InsertPageBefore(new CarouselPageMenu(), this);
            }
            await Task.Delay(100);

            await Navigation.PopAsync();
        }
    }
}
