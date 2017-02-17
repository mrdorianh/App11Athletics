using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    public partial class TabataOptions : ContentView
    {
        public TabataOptions()
        {
            InitializeComponent();
            ListTotalRounds = new List<string>();
            ListMinOn = new List<string>();
            ListSecOn = new List<string>();
            ListMinOff = new List<string>();
            ListSecOff = new List<string>();
            ListPickers = new List<Picker>();
            for (int i = 0; i < 100; i++)
            {

                ListTotalRounds.Add(i.ToString());
                if (i <= 59)
                {
                    ListMinOn.Add(i.ToString());
                    ListSecOn.Add(i.ToString());
                    ListMinOff.Add(i.ToString());
                    ListSecOff.Add(i.ToString());
                }

            }
            foreach (var listTotalRound in ListTotalRounds)
                pickerRounds.Items.Add(listTotalRound);
            foreach (var listMinOn in ListMinOn)
            {
                pickerMinOn.Items.Add(listMinOn);
            }
            foreach (var s in ListSecOn)
                pickerSecOn.Items.Add(s);
            foreach (var s in ListMinOff)
                pickerMinOff.Items.Add(s);
            foreach (var s in ListSecOff)
                pickerSecOff.Items.Add(s);

            FontSize = Width / 16;
            FontSizeLarge = Width / 10;
            FrameSize = Width / 5;
            ListPickers.Add(pickerRounds);
            ListPickers.Add(pickerMinOn);
            ListPickers.Add(pickerSecOn);
            ListPickers.Add(pickerMinOff);
            ListPickers.Add(pickerSecOff);
            foreach (var p in ListPickers)
            {
                if (p.SelectedIndex == -1)
                    p.SelectedIndex = 0;
                if (p.StyleId == "r")
                    p.SelectedIndex = 1;

            }
            pickerRounds.SelectedIndex = 5;
            pickerSecOn.SelectedIndex = 30;
            pickerSecOff.SelectedIndex = 30;
            CheckParameters();
        }

        private void CheckParameters()
        {
            Button_OnClicked(null, null);
            var picked = Convert.ToInt32(pickerRounds.Items[pickerRounds.SelectedIndex]);
            if (picked < 2 || TimeSpan.FromMinutes(TimeOnMinutes) + TimeSpan.FromSeconds(TimeOnSeconds) < TimeSpan.FromSeconds(5) || TimeSpan.FromMinutes(TimeOffMinutes) + TimeSpan.FromSeconds(TimeOffSeconds) < TimeSpan.FromSeconds(5))
            {
                Valid = false;
                buttonSave.IsEnabled = Valid;
                buttonSave.BackgroundColor = NewColor;
                if (picked < 2)
                {
                    labelTotalRounds.TextColor = NewColor;
                }
                else if (TimeSpan.FromMinutes(TimeOnMinutes) + TimeSpan.FromSeconds(TimeOnSeconds) < TimeSpan.FromSeconds(5))
                {
                    labelTimeOn.TextColor = NewColor;
                }
                else if (TimeSpan.FromMinutes(TimeOffMinutes) + TimeSpan.FromSeconds(TimeOffSeconds) <
                         TimeSpan.FromSeconds(5))
                {
                    labelTimeOff.TextColor = NewColor;
                }
            }
            else
            {
                Valid = true;
                buttonSave.BackgroundColor = OriginalColor;
                labelTotalRounds.TextColor = OriginalColor;
                labelTimeOn.TextColor = OriginalColor;
                labelTimeOff.TextColor = OriginalColor;
                buttonSave.IsEnabled = Valid;
            }
        }

        public Color OriginalColor => Color.FromHex("#029902");
        public Color NewColor => Color.FromHex("#DF0000");
        public string TabataOptionsHeader { get; set; }
        public double TotalRounds { get; set; }
        public double TimeOnSeconds { get; set; }
        public double TimeOffSeconds { get; set; }
        public double TimeOnMinutes { get; set; }
        public double TimeOffMinutes { get; set; }
        public double FontSize { get; set; }
        public double FontSizeLarge { get; set; }
        public double FrameSize { get; set; }
        public bool Valid { get; set; }


        public IList<string> ListTotalRounds;
        public IList<Picker> ListPickers;
        public IList<string> ListMinOn;
        public IList<string> ListMinOff;
        public IList<string> ListSecOn;
        public IList<string> ListSecOff;

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {


            CheckParameters();
        }

        private void TabataOptions_OnSizeChanged(object sender, EventArgs e)
        {
            FontSize = Width / 16;
            FontSizeLarge = Width / 10;
            FrameSize = Width / 5;
        }

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
            TimeOffMinutes = Convert.ToDouble(pickerMinOff.SelectedIndex.ToString());
            TimeOffSeconds = Convert.ToDouble(pickerSecOff.SelectedIndex.ToString());
        }
    }
}
