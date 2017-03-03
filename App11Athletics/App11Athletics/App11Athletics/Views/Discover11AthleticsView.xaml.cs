using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Models;
using App11Athletics.ViewModels;
using ImageCircle.Forms.Plugin.Abstractions;
using Plugin.Connectivity;
using Xamarin.Forms;
using App11Athletics.DHCToolkit;


namespace App11Athletics.Views
{
    public partial class Discover11AthleticsView : ContentPage
    {
        public Discover11AthleticsView()
        {
            InitializeComponent();
            trainersListView.ItemsSource = new Discover11AthleticsViewModel().ListCollectionTrainers;
            Opacity = 0;

        }

        #region Overrides of Page

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            disabled = true;
            this.FadeTo(1, 350U, Easing.CubicIn);
            await AnimatePages.AnimatePageIn(gridDiscover, null);
            disabled = false;
        }

        #region Overrides of Page

        #region Overrides of Page

        protected override bool OnBackButtonPressed()
        {
            return base.OnBackButtonPressed();

        }

        #endregion

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            await AnimatePages.AnimatePageOut(gridDiscover, null);

        }

        #endregion

        #endregion

        public async void TrainersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (disabled)
                return;
            if (((ListView)sender).SelectedItem == null)
                return;
            var selItem = ((ListView)sender).SelectedItem;

            Trainer TrainerItem =
                (from itm in Models.Discover11AthleticsModel.Trainers where itm.Equals(selItem) select itm).FirstOrDefault<Trainer>();
            var das = await DisplayActionSheet(TrainerItem.ShortName, "Cancel", null, "View Bio", "Email");
            if (!CrossConnectivity.Current.IsConnected)
            {
                ((ListView) sender).SelectedItem = null;
                return;
            }
            switch (das)
            {
                case "View Bio":

                    if (!string.IsNullOrEmpty(TrainerItem.BioUrl))
                        Device.OpenUri(new Uri(TrainerItem.BioUrl));
                    ((ListView)sender).SelectedItem = null;
                    break;

                case "Email":
                    if (!string.IsNullOrEmpty(TrainerItem.Email))
                        Device.OpenUri(new Uri(TrainerItem.Email));
                    ((ListView)sender).SelectedItem = null;
                    break;
                default:
                    ((ListView)sender).SelectedItem = null;
                    break;
            }
        }

        public bool disabled { get; set; }

        public void ContentPage_OnSizeChanged(object sender, EventArgs e)
        {

        }

        private void OnCall(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:16147452868"));
        }

        private async void Schedule_OnClicked(object sender, EventArgs e)
        {
            if (!CrossConnectivity.Current.IsConnected)
                return;
            await Navigation.PushAsync(new ScheduleView());

        }

        private void TrainersListView_OnChildAdded(object sender, ElementEventArgs e)
        {

        }
    }
}
