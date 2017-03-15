using System.ComponentModel;

using Xamarin.Forms;

namespace App11Athletics.Views.Timers
{
    public partial class TimerButtonsView : ContentView
    {
        public TimerButtonsView()
        {
            InitializeComponent();

        }

        //        private void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        //        {
        //            foreach (var view in gridbuttons.Children)
        //            {
        //                var button = (Button)view;
        //                button.IsVisible = button.IsEnabled;
        //            }
        //        }
        public void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var b = (Button)sender;
            b.IsVisible = b.IsEnabled;
        }
    }
}
