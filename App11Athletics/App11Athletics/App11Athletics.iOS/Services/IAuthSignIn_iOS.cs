using System;
using System.Threading.Tasks;
using App11Athletics.Helpers;
using App11Athletics.iOS.Services;
using Auth0.SDK;
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

        public async Task AuthSignIn()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
            {
                vc = vc.PresentedViewController;
            }
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
                var rt = Settings.UserRefreshToken;
                await client.RefreshToken(rt);
                var user = client.CurrentUser;
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

        public Task SaveUserAttributes(Auth0User user)
        {

            //            Settings.UserRefreshToken = user.RefreshToken;
            //            Settings.UserEmail = user.Profile["email"].ToString();
            //            Settings.UserGivenName = user.Profile["given_name"].ToString();
            //            Settings.UserFamilyName = user.Profile["family_name"].ToString();
            //            Settings.UserName = user.Profile["name"].ToString();
            //            Settings.UserPicture = user.Profile["picture"]?.ToString();
            //            Settings.UserAge = user.Profile["age"]?.ToString();
            //            Settings.UserGender = user.Profile["gender"].ToString();
            Settings.UserRefreshToken = user.RefreshToken;
            Settings.UserEmail = user.Profile["email"]?.ToString();
            var t = user.Profile["identities"];
            var c = t.Children();
            Settings.UserProvider = c["provider"]?.ToString();
            Settings.UserGivenName = user.Profile["given_name"]?.ToString();
            Settings.UserFamilyName = user.Profile["family_name"]?.ToString();
            Settings.UserName = user.Profile["name"]?.ToString();
            string pic = user.Profile["picture_large"]?.ToString();
            if (string.IsNullOrEmpty(pic))
                pic = user.Profile["picture"]?.ToString();
            Settings.UserPictureOriginal = pic;
            if (string.IsNullOrEmpty(Settings.UserPicture) || (Settings.UserPicture == "iconbevel.png"))
                Settings.UserPicture = pic;
            if (string.IsNullOrEmpty(Settings.UserAge))
                Settings.UserAge = user.Profile["age"]?.ToString();
            if (string.IsNullOrEmpty(Settings.UserGender))
                Settings.UserGender = user.Profile["gender"]?.ToString();
            return Task.CompletedTask;
        }

    }
}