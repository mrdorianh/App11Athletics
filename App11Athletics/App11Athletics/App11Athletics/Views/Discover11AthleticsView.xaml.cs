using System;
using System.Linq;
using System.Threading.Tasks;
using App11Athletics.Models;
using App11Athletics.ViewModels;
using Plugin.Connectivity;
using Xamarin.Forms;
using App11Athletics.DHCToolkit;
using PropertyChanged;


namespace App11Athletics.Views
{
    [ImplementPropertyChanged]
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
            await Task.Delay(100);
            await Task.WhenAll(this.FadeTo(1, 350U, Easing.CubicIn), AnimatePages.AnimatePageIn(gridDiscover));
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
            await AnimatePages.AnimatePageOut(gridDiscover);

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
                ((ListView)sender).SelectedItem = null;
                return;
            }
            switch (das)
            {
                case "View Bio":

                    if (!string.IsNullOrEmpty(TrainerItem.BioUrl))
                        Device.OpenUri(new Uri(TrainerItem.BioUrl));
                    break;

                case "Email":
                    if (!string.IsNullOrEmpty(TrainerItem.Email))
                        Device.OpenUri(new Uri(TrainerItem.Email));
                    break;
                default:
                    ((ListView)sender).SelectedItem = null;
                    break;
            }
             ((ListView)sender).SelectedItem = null;
        }

        public bool disabled { get; set; }
        public double CircleDiameter { get; set; } = 0.0;
        public void ContentPage_OnSizeChanged(object sender, EventArgs e) { CircleDiameter = Width / 3; }

        private void OnCall(object sender, EventArgs e)
        {
            Device.OpenUri(new Uri("tel:16147452868"));
        }

        public async void Schedule_OnClicked(object sender, EventArgs e)
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
