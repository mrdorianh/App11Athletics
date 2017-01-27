using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    public partial class ImageCircleProportional : ContentView
    {
        public ImageCircleProportional()
        {

            InitializeComponent();

        }

        public double SizeProp { get; set; }

        public void ImageCircleProportional_OnSizeChanged(object sender, EventArgs e)
        {
            var w = Width;
            var h = Height;

            if (h <= w)
            {
                SizeProp = h;
            }
            else if (w <= h)
            {
                SizeProp = w;
            }

        }
    }
}
