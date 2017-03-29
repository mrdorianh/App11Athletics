using App11Athletics.Data;
using App11Athletics.Helpers;
using App11Athletics.Views;
using Xamarin.Forms;

namespace App11Athletics
{
    public partial class App : Application
    {
        static TodoItemDatabase database;
        //        public UserProfileModel AppUser;
        public App()
        {
            InitializeComponent();
            /*IOS*/
            MainPage = new NavigationPage(new SplashWebView());
            /**/
            /*Droid*/
            //            if (string.IsNullOrEmpty(Settings.UserRefreshToken))
            //            {
            //                IsUserLoggedIn = false;
            //                MainPage = new NavigationPage(new LoginView());
            //            }
            //            else
            //            {
            //                IsUserLoggedIn = true;
            //                //                DependencyService.Get<IAuthSignIn>().AuthRefresh();
            //                MainPage = new NavigationPage(new HomeMenuView());
            //            }

        }


        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database =
                        new TodoItemDatabase(
                            DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }

                return database;
            }
        }

        public int ResumeAtTodoId { get; set; }
        public static bool IsUserLoggedIn { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}