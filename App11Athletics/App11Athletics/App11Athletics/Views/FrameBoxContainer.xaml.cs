using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Media;
using PropertyChanged;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class FrameBoxContainer : INotifyPropertyChanged
    {
        public FrameBoxContainer()
        {
            InitializeComponent();
        }

        public PanUpdatedEventArgs CPan
        {
            get { return null; }
            set { }
        }

        private void PanGestureRecognizer_OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            framexaml.TranslationX = e.TotalX;
            framexaml.TranslationY = e.TotalY;

        }
    }
}
