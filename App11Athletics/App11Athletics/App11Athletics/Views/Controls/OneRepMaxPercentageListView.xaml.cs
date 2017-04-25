using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Android.OS;
using App11Athletics.Models;
using Java.IO;
using Javax.Crypto.Spec;
using PropertyChanged;
using Xamarin.Forms;
using XFShapeView;

namespace App11Athletics.Views.Controls
{
    [ImplementPropertyChanged]
    public partial class OneRepMaxPercentageListView : ContentView
    {
        public ObservableCollection<string> PercentCollection;

        public OneRepMaxPercentageListView()
        {
            PercentCollection = new ObservableCollection<string>();
            for (double i = 50; i < 100; i = i + 2.5)
            {
                PercentCollection.Add(i.ToString());
            }
            InitializeComponent();
            percentageListView.ItemsSource = PercentCollection;
        }

        public double CircleWidth { get; set; }
        public Color SelectedColor = ((Color)Application.Current.Resources["ColorBrandGlobalBlue"]).MultiplyAlpha(0.5);


        private void PercentageListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            ((ListView)sender).SelectedItem = null;
        }
        public bool IsSelected { get; set; }
        public bool NotSelected => !IsSelected;
        public bool Deselected { get; set; }
        public string SelectedPercentage { get; set; }
        public string CurrentPercentage { get; set; }
        public double ActiveOpacity => Deselected ? 0.4 : 1;

        const string a = "AnimationText";
        private void OneRepMaxView_OnSizeChanged(object sender, EventArgs e)
        {
            if (CircleWidth > 0)
            {

            }
            CircleWidth = Width;

        }

        public void AddPercent(string p)
        {
            PercentCollection.Add(p);
        }

        public void ScrollReset()
        {
            var o = PercentCollection[0];
            percentageListView.ScrollTo(o, ScrollToPosition.Start, true);
        }
        public void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var s = (ShapeView)sender;

            CurrentPercentage = s.BindingContext.ToString();
            OnPercentageTapped?.Invoke(this);
            if (this.AnimationIsRunning(a))
                this.AbortAnimation(a);
            Animation parentAnimation = new Animation();
            Animation scaleInAnimation = new Animation(v => s.Scale = v, 0.8, 1, Easing.CubicInOut);
            parentAnimation.Add(0, 1, scaleInAnimation);
            parentAnimation.Commit(this, a, 16, 250U, Easing.CubicInOut);
        }

        public event ViewAction OnPercentageTapped;
        public delegate void ViewAction(OneRepMaxPercentageListView view);


        private void PercentageListView_OnItemTapped(object sender, ItemTappedEventArgs e)
        { }

        private void ClearTapped(object sender, EventArgs e)
        {
            var s = (ShapeView)sender;
            CurrentPercentage = string.Empty;
            OnPercentageTapped?.Invoke(this);
            if (this.AnimationIsRunning(a))
                this.AbortAnimation(a);
            Animation parentAnimation = new Animation();
            Animation scaleInAnimation = new Animation(v => s.Scale = v, 0.8, 1, Easing.CubicInOut);
            parentAnimation.Add(0, 1, scaleInAnimation);
            parentAnimation.Commit(this, a, 16, 250U, Easing.CubicInOut);
        }
    }

}
