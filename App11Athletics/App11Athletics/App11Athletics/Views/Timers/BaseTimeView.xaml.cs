using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using PropertyChanged;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App11Athletics.Views.Timers
{
    public partial class BaseTimeView : ContentView
    {
        public BaseTimeView()
        {
            InitializeComponent();

        }

        private void BaseTimeView_OnSizeChanged(object sender, EventArgs e)
        {
            if (!(this.Width > 0))
                return;
            foreach (var view1 in gridTime.Children)
            {
                var view = (Label)view1;
                view.FontSize = gridTime.Width / 6;
            }
        }
    }
}
