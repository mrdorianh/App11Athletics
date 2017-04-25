using System;
using App11Athletics.Helpers;
using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    public partial class OneRepMax : ContentView
    {
        public bool wdisabled;

        public OneRepMax()
        {
            InitializeComponent();
            //            myEntry.Text = Settings.UserOneRMLift;
            myEntryWeight.Text = Settings.UserOneRMWeight;
            WeightLifted = Settings.UserOneRMWeight;
            if (myEntry.Text == string.Empty)
            {
                myEntry.Text = "Add Exercise";
            }
            else
            {
                labelentry.TextColor = OriginalColor;
            }
        }

        public double TitleFontSize { get; set; }
        public double LiftFontSize { get; set; }
        public double RepFontSize { get; set; }
        public double StepperRepValue { get; set; }
        public string Lift { get; set; }
        public string WeightLifted { get; set; }
        public Color OriginalColor => Color.FromHex("#005EBF");
        public Color NewColor => Color.Silver;



        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {

            //            myEntry.Focus();
        }

        public event EventHandler Clicked
        {
            add { buttonSaveOneRepMax.Clicked += value; }
            remove { buttonSaveOneRepMax.Clicked -= value; }
        }

        private void OneRepMax_OnSizeChanged(object sender, EventArgs e)
        {
            var w = Width * 0.75;
            TitleFontSize = w / 10;
            LiftFontSize = w / 6;
            RepFontSize = w / 10;
        }

        private void MyEntry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (myEntry.Text == string.Empty)
            {
                labelentry.TextColor = NewColor;
            }
            else
            {
                labelentry.TextColor = OriginalColor;
            }
            if (myEntry.Text.Length > 6)
                LiftFontSize = Width / Convert.ToDouble(myEntry.Text.Length);
            else
            {
                LiftFontSize = Width / 6;
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (wdisabled)
                return;
            wdisabled = true;

            myEntryWeight.Focus();
        }

        private void MyEntryWeight_OnUnfocused(object sender, FocusEventArgs e)
        {
            wdisabled = false;
            if (myEntryWeight.Text.Length <= 0)
                myEntryWeight.Text = "0";


        }

        private void ButtonSaveOneRepMax_OnClicked(object sender, EventArgs e)
        {
            Settings.UserOneRMLift = labelentry.Text;
            Settings.UserOneRMWeight = myEntryWeight.Text;
            StepperRepValue = stepperReps.Value;
            Lift = Settings.UserOneRMLift;
            WeightLifted = Settings.UserOneRMWeight;

        }

        public event EventHandler WClicked
        {
            add { buttonWeight.Clicked += value; }
            remove { buttonWeight.Clicked -= value; }
        }
        public event EventHandler LiftClicked
        {
            add { tapGestureRecognizerLabelEntry.Tapped += value; }
            remove { tapGestureRecognizerLabelEntry.Tapped -= value; }
        }
        public event EventHandler<FocusEventArgs> WUnfocused
        {
            add { myEntryWeight.Unfocused += value; }
            remove { myEntryWeight.Unfocused -= value; }
        }
        public event EventHandler<FocusEventArgs> WFocused
        {
            add { myEntryWeight.Focused += value; }
            remove { myEntryWeight.Focused -= value; }
        }

        private void MyEntryWeight_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (myEntryWeight.Text.Length > 0)
                WeightLifted = myEntryWeight.Text + " lbs";

            else
            {
                WeightLifted = "...";
            }
        }

        public void FocusEntry()
        {
            myEntry.Focus();
        }

        private void MyEntry_OnFocused(object sender, FocusEventArgs e)
        {
            myEntry.Text = string.Empty;
            labelentry.Opacity = 1;
        }

        private void MyEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
            if (myEntry.Text == string.Empty)
            {
                labelentry.Opacity = 0.4;
                myEntry.Text = "Add Exercise";
            }
            else
            {
                labelentry.Opacity = 1;
                labelentry.TextColor = OriginalColor;
            }
        }
    }
}
