using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;
using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    [ImplementPropertyChanged]
    public partial class OneRepMaxViewCellView : ContentView
    {
        public OneRepMaxViewCellView()
        {
            InitializeComponent();
        }

        public string Lift { get; set; }
        public string Max { get; set; }
        public string RefMax { get; set; }

        public double BadgeFontSize
        {
            get { return 0; }
        }

        public double BadgeLength
        {
            get
            {
                if (gridCell != null)
                    return gridCell.Height / 3;
                return 0;
            }
        }

        public double BadgeX
        {
            get
            {
                var d = Width / 2 - 60;
                return d <= 0 ? 0 : d;
            }
            set
            {
                if (value <= 0)
                    ForceLayout();
            }
        }

        public double BadgeY
        {
            get
            {
                if (gridCell != null)
                    return gridCell.Height / 2;
                return 0;
            }
        }




        //                
        private void OneRepMaxViewCellView_OnSizeChanged(object sender, EventArgs e)
        {

        }
    }
}
