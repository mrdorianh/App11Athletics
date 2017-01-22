using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.iOS;
using App11Athletics;
using App11Athletics.Helpers;
using App11Athletics.iOS.Services;
using Auth0.SDK;
using Xamarin.Forms.Platform.iOS;
using Foundation;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(IAuthSignIn_iOS))]
namespace App11Athletics.iOS.Services
{
    public class IAuthSignIn_iOS : IAuthSignIn
    {
        public IAuthSignIn_iOS() { }

        public Auth0.SDK.Auth0Client client = new Auth0.SDK.Auth0Client("app11athletics.auth0.com",
            "yPZw5Yv1WZbLdASc6Rkaz2Ib9e4iDqGW");

        #region Implementation of IAuthSignIn

        public Task AuthSignIn()
        {
            return null;
        }

        public async Task AuthLogOut()
        {
            App.IsUserLoggedIn = false;
            Settings.UserRefreshToken = string.Empty;
            await Task.Run(() =>
               { client.Logout(); });
        }

        public async Task AuthRefresh()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }
            try
            {
                await client.RefreshToken();
                var user = this.client.CurrentUser;
                if (!string.IsNullOrEmpty(user?.RefreshToken))
                {
                    App.IsUserLoggedIn = true;
                    SaveUserAttributes(user);
                }
                else
                    App.IsUserLoggedIn = false;
            }
            catch (Exception e)
            {
                try
                {
                    await this.client.LoginAsync(vc, withRefreshToken: true);
                    var user = this.client.CurrentUser;
                    if (!string.IsNullOrEmpty(user.RefreshToken))
                    {
                        App.IsUserLoggedIn = true;
                        SaveUserAttributes(user);
                    }
                    else
                        App.IsUserLoggedIn = false;
                }
                catch (Exception exception)
                {
                    Settings.UserRefreshToken = string.Empty;
                }
            }
        }
        #endregion
        public void SaveUserAttributes(Auth0User user)
        {

            Settings.UserRefreshToken = user.RefreshToken;
            Settings.UserEmail = user.Profile["email"].ToString();
            Settings.UserGivenName = user.Profile["given_name"].ToString();
            Settings.UserPicture = user.Profile["picture"].ToString();
            Settings.UserAge = user.Profile["age"].ToString();
            Settings.UserFamilyName = user.Profile["family_name"].ToString();
            var rawGender = user.Profile["gender"].ToString();
            if (rawGender == "male")
                Settings.UserGender = "Male";
            else if (rawGender == "female")
            {
                Settings.UserGender = "Female";
            }

        }

    }
}