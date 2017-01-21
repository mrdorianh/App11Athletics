using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public partial class UserProfileView : ContentPage
    {
        public UserProfileView()
        {
            InitializeComponent();
            PageLabels = new List<Label>();
            PageLabels.AddRange(new[]
            {
                labelAge, labelGender, labelHeightFt, labelHeightInMark, labelHeightInch, labelHeightftMark,
                labelWeight, labelActivityLevel, labelBmr, labelDCE
            });
        }

        public List<Label> PageLabels { get; set; }

        public ICommand labelchange { get; set; }

        public double DFontSize { get; private set; }

        private void UserProfileView_OnSizeChanged(object sender, EventArgs e)
        {
            if (PageLabels == null)
                return;
            DFontSize = gridGender.Width / 3;
            var n = gridHeader.Width * 0.5 / 7;
            labelName.FontSize = n;
            foreach (var label in PageLabels)
            {
                label.FontSize = DFontSize;
            }
        }
    }
}