using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Reset the 'resume' id, since we just want to re-start here
            ((App)App.Current).ResumeAtTodoId = -1;
            listView.ItemsSource = await App.Database.GetItemsAsync();
        }

        async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutLogOptionsView { BindingContext = new TodoItem() });
        }

        async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((App)App.Current).ResumeAtTodoId = (e.SelectedItem as TodoItem).ID;
            Debug.WriteLine("setting ResumeAtTodoId = " + (e.SelectedItem as TodoItem).ID);
            await Navigation.PushAsync(new WorkoutLogOptionsView { BindingContext = e.SelectedItem as TodoItem });
        }

        private void ListView_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (App.Database.GetItemsAsync().Result.Count > 0)
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
                catch (NullReferenceException) { }
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