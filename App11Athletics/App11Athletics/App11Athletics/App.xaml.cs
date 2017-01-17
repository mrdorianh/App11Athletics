using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App11Athletics.Views;
using App11Athletics.Views.Timers;
using Xamarin.Forms;
using StopwatchFeatureView = App11Athletics.Views.Timers.StopwatchFeatureView;

namespace App11Athletics
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TabataFeatureView();
        }

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
