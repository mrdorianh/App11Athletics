using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Bluetooth;
using App11Athletics.ViewModels.Timers;
using Xamarin.Forms;

namespace App11Athletics.Views.Timers
{
    public partial class TabataFeatureView : ContentPage
    {
        public TabataFeatureView()
        {
            InitializeComponent();

            if (labelAnimateStatus.Text != null)
                WorkTimePrevious = labelAnimateStatus.Text;
            WorkTimeText = WorkTimePrevious;

        }

        public static bool WorkTimeBool { get; set; }
        public static string WorkTimeText { get; set; } = string.Empty;
        public static string WorkTimePrevious { get; set; } = string.Empty;



        private async void BindableObject_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var label = (Label)sender;
            if (label != null)
            {
                label.AnchorY = 0;
            }
            if (labelAnimateStatus?.Text != null)
                WorkTimeText = labelAnimateStatus.Text;
            if (WorkTimeText == WorkTimePrevious)
                return;

            if (WorkTimeText == "Work Time!")
            {
                statusAnimation.AnimateWorkTimeTask(1);
            }
            else
            {
                statusAnimation.AnimateWorkTimeTask(0);
            }
            

            Debug.WriteLine("animating");
            WorkTimePrevious = WorkTimeText;

        }

        private void LabelAnimateStatus_OnPropertyChanging(object sender, PropertyChangingEventArgs e)
        {



        }
    }
}
