using System;

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
                if (view.StyleId != "titles")
                    view.FontSize = Width / 6.4;
                else
                {
                    view.FontSize = Width / 4 / 10;
                }
            }
        }
    }
}
