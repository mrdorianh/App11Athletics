using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    public partial class RoundOptions : ContentView
    {
        public RoundOptions()
        {
            InitializeComponent();
            ListTotalRounds = new List<string>();
            ListPickers = new List<Picker>();
            ListMinOn = new List<string>();
            ListSecOn = new List<string>();
            for (int i = 0; i < 100; i++)
            {
                ListTotalRounds.Add(i.ToString());
                if (i <= 59)
                {
                    ListMinOn.Add(i.ToString());
                    ListSecOn.Add(i.ToString());
                }
            }
            ListPickers.Add(pickerRounds);
            ListPickers.Add(pickerMinOn);
            ListPickers.Add(pickerSecOn);
            foreach (var listTotalRound in ListTotalRounds)
                pickerRounds.Items.Add(listTotalRound);
            foreach (var listMinOn in ListMinOn)
                pickerMinOn.Items.Add(listMinOn);

            foreach (var s in ListSecOn)
                pickerSecOn.Items.Add(s);
            foreach (var p in ListPickers)
            {
                if (p.SelectedIndex == -1)
                    p.SelectedIndex = 0;
                if (p.StyleId == "r")
                    p.SelectedIndex = 1;

            }
            FontSize = Width / 16;
            FontSizeLarge = Width / 10;
            FrameSize = Width / 5;
            pickerSecOn.SelectedIndex = 5;

        }
        public double TotalRounds { get; set; }
        public double FontSize { get; set; }
        public double TimeOnMinutes { get; set; }
        public double TimeOnSeconds { get; set; }
        public double FontSizeLarge { get; set; }
        public double FrameSize { get; set; }
        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e) { }
        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            var box = (BoxView)sender;
            var id = box.StyleId;
            foreach (var p in ListPickers)
            {
                if (p.StyleId != id)
                    continue;
                var picker = p;
                picker.Focus();
                break;
            }
        }
        public IList<string> ListTotalRounds;
        public IList<Picker> ListPickers;
        public IList<string> ListMinOn;
        public IList<string> ListSecOn;
        public event EventHandler Clicked
        {
            add { buttonSave.Clicked += value; }
            remove { buttonSave.Clicked -= value; }
        }
        private void Button_OnClicked(object sender, EventArgs e)
        {
            TotalRounds = Convert.ToDouble(pickerRounds.SelectedIndex.ToString());
            TimeOnMinutes = Convert.ToDouble(pickerMinOn.SelectedIndex.ToString());
            TimeOnSeconds = Convert.ToDouble(pickerSecOn.SelectedIndex.ToString());
        }

        private void RoundOptions_OnSizeChanged(object sender, EventArgs e)
        {
            FontSize = Width / 16;
            FontSizeLarge = Width / 10;
            FrameSize = Width / 5;
        }
    }
}
