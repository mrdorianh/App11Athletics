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
            oneRepMaxView.TranslationY = 1500;
            viewWeightOptions.TranslationY = -1000;
        }

        public string MaxLift { get; set; }
        public string MaxWeight { get; set; }

        public double OneRepMaxFontSize { get; set; }
        public double WOneRepMaxFontSize { get; set; }

        public double labelWeightOptionsFontSize { get; set; }

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

        private void TapGestureRecognizerOneRepMax_OnTapped(object sender, EventArgs e)
        {
            oneRepMaxView.TranslateTo(0, 0, 350U, Easing.CubicOut);
            oneRepMaxView.Clicked += OneRepMaxView_Clicked;
            oneRepMaxView.FocusEntry();
        }

        private void OneRepMaxView_Clicked(object sender, EventArgs e)
        {
            labelMaxLift.Text = oneRepMaxView.Lift;
            labelMaxWeight.Text = OneRepMaxCalc(oneRepMaxView.WeightLifted, oneRepMaxView.StepperRepValue) + " lbs";
            oneRepMaxView.TranslateTo(0, 1500, 350U, Easing.CubicIn);

            WorkoutLogListView_OnSizeChanged(null, null);


        }
        private string OneRepMaxCalc(string weightlifted, double reps)
        {
            var dweightLifted = Convert.ToDouble(weightlifted);

            var Max = (dweightLifted / (1.0278 - (0.0278 * reps)));

            return Convert.ToInt32(Max).ToString();
        }

        private void WorkoutLogListView_OnSizeChanged(object sender, EventArgs e)
        {
            if (labelMaxLift.Text != null && labelMaxLift.Text.Length > 12)
                OneRepMaxFontSize = gridOneRepMax.Width / (labelMaxLift.Text.Length);

            else
            {
                OneRepMaxFontSize = gridOneRepMax.Width / 12;
            }
            WOneRepMaxFontSize = Width / 12;
            labelWeightOptionsFontSize = Width / 12;
            labelMaxLift.FontSize = OneRepMaxFontSize;
            labelMaxWeight.FontSize = WOneRepMaxFontSize;

        }

        private void OneRepMaxView_OnWClicked(object sender, EventArgs e)
        {
            viewWeightOptions.TranslateTo(0, 0, 350U, Easing.CubicOut);
        }

        private async void OneRepMaxView_OnWUnfocused(object sender, FocusEventArgs e)
        {
            viewWeightOptions.TranslateTo(0, -1000, 350U, Easing.CubicIn);

            await oneRepMaxView.FadeTo(1, 350U, Easing.CubicIn);
            listView.FadeTo(1, 350U, Easing.CubicIn);
        }

        private void OneRepMaxView_OnWFocused(object sender, FocusEventArgs e)
        {
            oneRepMaxView.FadeTo(0.2, 350U, Easing.CubicIn);
            listView.FadeTo(0, 350U, Easing.CubicIn);
        }
    }
}