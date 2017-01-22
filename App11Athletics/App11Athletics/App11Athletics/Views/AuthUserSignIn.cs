using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using App11Athletics.Helpers;
using App11Athletics.Views;
using Xamarin.Forms;

namespace App11Athletics.ViewModels
{
    public class AuthUserSignIn : ContentPage
    {
        public AuthUserSignIn()
        {
            LoggedInNavigate();
        }

        private async void LoggedInNavigate()
        {
            await DependencyService.Get<IAuthSignIn>().AuthRefresh();
            if (!App.IsUserLoggedIn)
            {
                Navigation.InsertPageBefore(new Discover11AthleticsView(), this);
                await Navigation.PopModalAsync();
            }
            else
            {
                await Navigation.PopModalAsync();
                //lets test this later as Reset Main
                //                Application.Current.MainPage = new NavigationPage(new WorkoutLogListView());
                //Navigation.InsertPageBefore(new CarouselPageMenu(), this);
            }


        }
    }
}

