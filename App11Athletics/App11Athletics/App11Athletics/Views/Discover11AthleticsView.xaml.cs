using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.ViewModels;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;


namespace App11Athletics.Views
{
    public partial class Discover11AthleticsView : ContentPage
    {
        public Discover11AthleticsView()
        {
            InitializeComponent();
            trainersListView.ItemsSource = new Discover11AthleticsViewModel().ListCollectionTrainers;

        }


        public void TrainersListView_ItemSelected(object sender, SelectedItemChangedEventArgs e) { }

        public void ContentPage_OnSizeChanged(object sender, EventArgs e)
        {

        }

        private void VisualElement_OnSizeChanged(object sender, EventArgs e)
        {
            var imagecircle = (VisualElement)sender;
            var parent = imagecircle.Parent;



        }
    }
}
