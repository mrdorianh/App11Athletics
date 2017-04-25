using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.App.Admin;
using Android.OS;
using Android.Renderscripts;
using Android.Runtime;
using App11Athletics.Helpers;
using App11Athletics.Models;
using App11Athletics.Views.Controls;
using Java.Lang.Ref;
using PropertyChanged;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App11Athletics.Views
{
    public partial class OneRepMaxList : ContentPage
    {
        public OneRepMaxList()
        {
            InitializeComponent();
            percentageList.Deselected = true;
            percentageList.ScrollReset();
            CurrentItem = new TodoItem();
        }
        protected override async void OnAppearing()
        {
            CurrentItem = null;
            base.OnAppearing();
            ((App)Application.Current).ResumeAtTodoId = -1;
            var itemSource = await App.Database.GetFilteredItemsAsync(true);
            listView.ItemsSource = itemSource;
            foreach (TodoItem item in listView.ItemsSource)
                item.IsSelected = false;
        }

        private async void OnItemAdded(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new WorkoutLogOptionsView(null, true) { BindingContext = new TodoItem() });
            //            var debugitem = new TodoItem { Name = "debug", IsMaxLift = true };
            //            await App.Database.SaveItemAsync(debugitem);

        }




        private async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //ItemSelected is called on deselection
            ((ListView)sender).SelectedItem = null;

            #region WorkingButLowPerformance
            /*                  /////This worked ok
                                                var selected = e.SelectedItem as TodoItem;
                                                if (selected?.ID == CurrentItem.ID)
                                                {
                                                    await Task.Delay(1);
                                        ((ListView)sender).SelectedItem = null;
                                                };
                                    IsFirstTap = true;
            */

            #endregion
        }

        public static bool IsFirstTap;

        private async void ListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {


            var itm = e.Item as TodoItem;
            CurrentItem = itm;
            Device.BeginInvokeOnMainThread(() =>
            {
                if (itm == null)
                    return;

                itm.IsSelected = !itm.IsSelected;

                foreach (TodoItem item in listView.ItemsSource)
                {
                    if (item != itm)
                        item.IsSelected = false;
                    else if (item == itm && item.IsSelected)
                    {
                        percentageList.Deselected = false;

                    }
                    else
                    {
                        percentageList.Deselected = true;
                        percentageList.ScrollReset();
                        CurrentItem = null;
                    }

                }
            });


        }

        public bool disbled { get; set; }

        public static TodoItem CurrentItem { get; set; }

        private static async Task DeselectListItem(object sender)
        {
            Task.Run(() => ((ListView)sender).SelectedItem = null);
        }
        public double BadgeX
        {
            get { return Width * .80 / 2 - 60; }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(value));
            }
        }
        public static string SelectedLift { get; set; }

        public static bool toggled;

        private async void RemoveItem_OnClicked(object sender, EventArgs e)
        {
            if (percentageList.Deselected)
            {
                await DisplayAlert("Please make a selection", null, "Ok");
                return;
            }

            var das = await DisplayActionSheet("Remove Item?", "Cancel", null, "Yes");

            switch (das)
            {
                case "Yes":
                    foreach (TodoItem t in listView.ItemsSource)
                    {
                        if (t.IsSelected)
                            await App.Database.DeleteItemAsync(t);
                    }

                    listView.ItemsSource = await App.Database.GetFilteredItemsAsync(true);
                    percentageList.Deselected = true;
                    percentageList.ScrollReset();
                    CurrentItem = null;
                    break;
            }
        }


        private async void MenuItem_OnClicked(object sender, EventArgs e)
        {

            listView.ItemsSource = await App.Database.GetFilteredItemsAsync(true);
        }

        private void LPMenuItem_OnClicked(object sender, EventArgs e)
        {
            listView.SelectedItem = null;
        }

        private void PercentageList_OnPercentageTapped(object sender, ItemTappedEventArgs e)
        {
            if (CurrentItem.IsSelected)
                CurrentItem.RefPercent = percentageList.CurrentPercentage;




        }

        private void PercentageList_OnOnPercentageTapped(OneRepMaxPercentageListView view)
        {
            if (CurrentItem == null)
                return;
            CurrentItem.RefPercent = percentageList.CurrentPercentage;
            App.Database.SaveItemAsync(CurrentItem);
        }

        async void Edit_OnClicked(object sender, EventArgs e)
        {
            if (percentageList.Deselected || (CurrentItem == null))
            {
                await DisplayAlert("Please make a selection", null, "Ok");
                return;
            }
            await Navigation.PushAsync(new WorkoutLogOptionsView(null, true) { BindingContext = CurrentItem });

        }
    }



}
