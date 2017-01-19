using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Models;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class WorkoutLogListView : ContentPage
    {
        public WorkoutLogListView()
        {
            InitializeComponent();

             #region toolbar

            ToolbarItem tbi = null;
            if (Device.OS == TargetPlatform.iOS)
            {
                tbi = new ToolbarItem("+", null, () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new WorkoutLogOptionsView();
                    todoPage.BindingContext = todoItem;
                    Navigation.PushAsync(todoPage);
                }, 0, 0);
            }
            if (Device.OS == TargetPlatform.Android)
            {
                // BUG: Android doesn't support the icon being null
                tbi = new ToolbarItem("+", "plus", () =>
                {
                    var todoItem = new TodoItem();
                    var todoPage = new WorkoutLogOptionsView();
                    todoPage.BindingContext = todoItem;
                    Navigation.PushAsync(todoPage);
                }, 0, 0);
            }
            ToolbarItems.Add(tbi);

            if (Device.OS == TargetPlatform.iOS)
            {
                var tbi2 = new ToolbarItem("?", null, () =>
                {
                    var todos = App.Database.GetItemsNotDone();
                    var tospeak = "";
                    foreach (var t in todos)
                        tospeak += t.Name + " ";
                    if (tospeak == "")
                        tospeak = "there are no tasks to do";

                    DependencyService.Get<ITextToSpeech>().Speak(tospeak);
                }, 0, 0);
                ToolbarItems.Add(tbi2);
            }

            #endregion
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // reset the 'resume' id, since we just want to re-start here
            ((App) Application.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = App.Database.GetItems();
            if (listView.ItemsSource != null) labelEmptyList.IsVisible = false;
            else labelEmptyList.IsVisible = true;
        }

        void listItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var todoItem = (TodoItem) e.SelectedItem;
            var todoPage = new WorkoutLogOptionsView {BindingContext = todoItem};

            ((App) Application.Current).ResumeAtTodoId = todoItem.ID;
            Debug.WriteLine("setting ResumeAtTodoId = " + todoItem.ID);

            Navigation.PushAsync(todoPage);
        }

        private void TodoItemListX_OnPropertyChanging(object sender, PropertyChangingEventArgs e) {}

        private void ListView_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (App.Database.Count > 0)
            {
                try
                {
                    if (labelEmptyList != null)
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            labelEmptyList.IsVisible = false;
                            boxViewBack.IsVisible = false;
                        }); 
                    }
                }
                catch (NullReferenceException) {}
            }
            else
            {
                if (labelEmptyList != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        labelEmptyList.IsVisible = true;
                        boxViewBack.IsVisible = true;
                    });
                }
            }
        }
    }
}