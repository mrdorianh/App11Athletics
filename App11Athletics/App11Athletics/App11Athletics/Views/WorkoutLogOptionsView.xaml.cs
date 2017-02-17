using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Helpers;
using App11Athletics.Models;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class WorkoutLogOptionsView : ContentPage
    {
        public WorkoutLogOptionsView()
        {
            InitializeComponent();
        }

        async void OnSaveClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.Database.SaveItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnDeleteClicked(object sender, EventArgs e)
        {
            var todoItem = (TodoItem)BindingContext;
            await App.Database.DeleteItemAsync(todoItem);
            await Navigation.PopAsync();
        }

        async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        //        void OnSpeakClicked(object sender, EventArgs e)
        //        {
        //            var todoItem = (TodoItem)BindingContext;
        //            DependencyService.Get<ITextToSpeech>().Speak(todoItem.Name + " " + todoItem.Notes);
        //        }

        private void NameEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (nameEntry != null && nameEntry.Text.Length < 1)
            {
                buttonSave.IsEnabled = false;
            }
            else
            {
                if (buttonSave != null)
                    buttonSave.IsEnabled = true;
            }
        }

        private void SetEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var numEntry = (Entry)sender;
            var numEntryText = numEntry.Text;
            var restrictCount = 2;
            if (numEntryText != null && numEntryText.Length > restrictCount)
            {
                // If it is more than your character restriction
                numEntryText = numEntryText.Remove(numEntryText.Length - 1); // Remove Last character
                numEntry.Text = numEntryText; // Set the Old value
            }

            if (string.IsNullOrEmpty(numEntry.Text) || numEntry.Text == "0")
            {
                numEntry.Text = null;
            }
        }

        private void RepEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            var numEntry = (Entry)sender;
            var numEntryText = numEntry.Text;
            var restrictCount = 2;
            if (numEntryText != null && numEntryText.Length > restrictCount)
            {
                // If it is more than your character restriction
                numEntryText = numEntryText.Remove(numEntryText.Length - 1); // Remove Last character
                numEntry.Text = numEntryText; // Set the Old value
            }

            if (string.IsNullOrEmpty(numEntry.Text) || numEntry.Text == "0")
            {
                numEntry.Text = null;
            }
        }

        private void SetEntry_OnFocused(object sender, FocusEventArgs e)
        {
            var t = (Entry)sender;
            Device.BeginInvokeOnMainThread(() => { t.Text = null; });
        }

        private void RepEntry_OnFocused(object sender, FocusEventArgs e)
        {
            var t = (Entry)sender;
            Device.BeginInvokeOnMainThread(() => { t.Text = null; });
        }

        private void NameEntry_OnPropertyChanging(object sender, PropertyChangingEventArgs e) { }
    }
}