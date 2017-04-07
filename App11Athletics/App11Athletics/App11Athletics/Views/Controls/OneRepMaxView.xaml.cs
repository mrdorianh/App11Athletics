using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;
using Android.OS;
using Java.IO;
using Xamarin.Forms;
using XFShapeView;

namespace App11Athletics.Views.Controls
{
    public partial class OneRepMaxView : ContentView
    {
        public ObservableCollection<string> PercentCollection;

        public OneRepMaxView()
        {
            PercentCollection = new ObservableCollection<string>();
            for (double i = 50; i < 100; i = i + 2.5)
            {
                PercentCollection.Add(i.ToString());
            }
            InitializeComponent();
            CircleWidth = Width / 4.2;


            percentageListView.ItemsSource = PercentCollection;


        }

        public double CircleWidth { get; set; }
        public Color SelectedColor = ((Color)Application.Current.Resources["ColorBrandGlobalBlue"]).MultiplyAlpha(0.5);


        private void PercentageListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private void PercentageListView_OnSizeChanged(object sender, EventArgs e)
        {

        }

        private void ShapeView_OnSizeChanged(object sender, EventArgs e) { }

        public string SelectedPercentage { get; set; }

        const string a = "AnimationText";
        private void OneRepMaxView_OnSizeChanged(object sender, EventArgs e)
        {
            if (CircleWidth > 0)
            {

            }
            CircleWidth = Width / 4.1;

        }

        private void PercentageListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {


            var s = (ShapeView)sender;

            SelectedPercentage = s.BindingContext.ToString();
            oneRepMaxControl.WeightLifted = SelectedPercentage;
            if (this.AnimationIsRunning(a))
                this.AbortAnimation(a);
            Animation parentAnimation = new Animation();
            Animation scaleInAnimation = new Animation(v => s.Scale = v, 0.8, 1, Easing.CubicInOut);
            parentAnimation.Add(0, 1, scaleInAnimation);

            parentAnimation.Commit(this, a, 16, 250U, Easing.CubicInOut);

        }

        private void ShapeView_OnFocused(object sender, FocusEventArgs e) { }
        private void ShapeView_OnUnfocused(object sender, FocusEventArgs e) { }
    }

    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _place;

        public string Place
        {
            get { return _place; }
            set
            {
                if (_place != value)
                {
                    _place = value;
                    OnPropertyChanged("Place");
                }
            }
        }

        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

}
