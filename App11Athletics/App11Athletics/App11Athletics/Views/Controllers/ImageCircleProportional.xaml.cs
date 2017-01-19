using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageCircle.Forms.Plugin.Abstractions;
using PropertyChanged;
using Xamarin.Forms;

namespace App11Athletics.Views.Controllers
{
    public partial class ImageCircleProportional : INotifyPropertyChanged
    {
        public double ViewWidth => Width;

        public double ViewHeight => ViewWidth;

        public ImageCircleProportional()
        {
            InitializeComponent();
        }
    }
}
