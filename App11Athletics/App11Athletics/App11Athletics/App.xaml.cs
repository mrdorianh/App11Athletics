﻿using System;
using App11Athletics.Annotations;
using App11Athletics.Data;
using App11Athletics.Helpers;
using App11Athletics.Views;
using App11Athletics.Views.Controls;
using FFImageLoading;
using FFImageLoading.Config;
using FFImageLoading.Forms;
using FFImageLoading.Forms.Args;
using FFImageLoading.Work;
using Xamarin.Forms;
using XLabs.Ioc;

namespace App11Athletics
{
    [PropertyChanged.ImplementPropertyChanged]
    public partial class App : Application
    {
        static TodoItemDatabase database;
        //        public UserProfileModel AppUser;
        public static bool IsAsleep;
        public App()
        {

            InitializeComponent();
            ImageService.Instance.Initialize();

            Device.OnPlatform(iOS: () =>
            {
                MainPage = new NavigationPage(new SplashWebView());
            }, Android: () =>
            {
                if (string.IsNullOrEmpty(Settings.UserRefreshToken))
                {
                    IsUserLoggedIn = false;
                    MainPage = new NavigationPage(new LoginView());
                }
                else
                {
                    IsUserLoggedIn = true;
                    //                            DependencyService.Get<IAuthSignIn>().AuthRefresh();
                    MainPage = new NavigationPage(new HomeMenuView());
                }
            });

        }


        public static string LogDate(DateTime dateTime)
        {
            return dateTime.ToString("dddd MMMM dd yyyy");
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
            IsAsleep = false;

            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            IsAsleep = true;
            //            Resolver.ResetResolver();
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            IsAsleep = false;
            // Handle when your app resumes
        }
    }
}