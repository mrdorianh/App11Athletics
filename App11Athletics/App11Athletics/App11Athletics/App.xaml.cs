using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App11Athletics.Data;
using App11Athletics.Helpers;
using App11Athletics.Views;
using App11Athletics.Views.Timers;
using Xamarin.Forms;
using StopwatchFeatureView = App11Athletics.Views.Timers.StopwatchFeatureView;

namespace App11Athletics
{
    public partial class App : Application
    {
        static TodoItemDatabase database;

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new WorkoutLogListView());
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