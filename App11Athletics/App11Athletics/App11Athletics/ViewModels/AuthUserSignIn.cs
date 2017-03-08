using App11Athletics.Helpers;
using Xamarin.Forms;

namespace App11Athletics.ViewModels
{
    public class AuthUserSignIn
    {
        public AuthUserSignIn()
        {
            //            LoggedInNavigate();
        }

        public void LoggedInNavigate()
        {
            DependencyService.Get<IAuthSignIn>().AuthRefresh();
            //            if (!App.IsUserLoggedIn)
            //            {
            //
            //                //                Application.Current.MainPage = new NavigationPage(new LoginView());
            //                //                await Navigation.PopModalAsync();
            //            }
            //            else
            //            {
            //
            //                // lets test this later as Reset Main
            //                // Application.Current.MainPage = new NavigationPage(new WorkoutLogListView());
            //                // Navigation.InsertPageBefore(new CarouselPageMenu(), this);
            //            }
        }
    }
}