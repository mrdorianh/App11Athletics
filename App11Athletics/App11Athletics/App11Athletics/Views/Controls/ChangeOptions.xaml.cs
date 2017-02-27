using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Media.Audiofx;
using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    public partial class ChangeOptions : ContentView
    {
        public ChangeOptions()
        {
            InitializeComponent();
            //            OptionsPlaceholder = "";
            //            OptionsFontSize = 0;

        }

        public string OptionsPlaceholder { get; set; }
        public string OptionsInput { get; set; }
        public int OptionsMaxLength { get; set; }
        public bool OptionsFocused { get; set; }
        public string OptionsOutput { get; set; }
        public double OptionsFontSize { get; set; }
        public double OptionsPlaceHolderFontSize { get; set; }
        public string HeightFtOutput { get; set; }
        public string HeightInOutput { get; set; }
        public double HeightFtValue { get; set; }
        public double HeightInValue { get; set; }
        public string GenderOutput { get; private set; }

        public Keyboard OptionsKeyboard { get; set; }

        public bool EntryVisible { get; set; }
        public bool AgeVisible { get; set; }
        public bool HeightVisible { get; set; }

        public bool GenderVisible { get; set; }

        public double StepperWidth { get; set; }

        public void Entry_OnUnfocused(object sender, FocusEventArgs e)
        {
            OptionsOutput = entry.Text;
        }

        public new bool Focus()
        {
            return entry.Focus();
        }

        public new void Unfocus()
        {
            entry.Unfocus();
        }



        public event EventHandler<FocusEventArgs> AgeUnfocused
        {
            add { entry.Unfocused += value; }
            remove { entry.Unfocused -= value; }
        }
        public event EventHandler<FocusEventArgs> WeightUnfocused
        {
            add { entry.Unfocused += value; }
            remove { entry.Unfocused -= value; }
        }

        public void Entry_OnCompleted(object sender, EventArgs e)
        {
            OptionsOutput = entry.Text;
            //            if (entry.IsFocused)
            //                entry.Unfocus();
        }

        private void ChangeOptions_OnSizeChanged(object sender, EventArgs e)
        {
            OptionsFontSize = Width / 6;
            OptionsPlaceHolderFontSize = Width / 8;
            StepperWidth = gridHeightSteppers.Width / 2.1;
        }

        private void StepperHeightIn_OnValueChanged(object sender, ValueChangedEventArgs e) { }
        private void StepperHeightFt_OnValueChanged(object sender, ValueChangedEventArgs e) { }

        private void ButtonEnterHeight_OnClicked(object sender, EventArgs e)
        {
            HeightFtOutput = stepperHeightFt.Value.ToString();
            HeightInOutput = stepperHeightIn.Value.ToString();

        }


        public event EventHandler MaleClicked
        {
            add
            {
                buttonFemale.Clicked += value;
                buttonMale.Clicked += value;
            }
            remove
            {
                buttonFemale.Clicked -= value;
                buttonMale.Clicked -= value;
            }
        }

        //        public event EventHandler FemaleClicked
        //        {
        //            add { buttonMale.Clicked += value; }
        public event EventHandler HeightClicked
        {
            add { buttonEnterHeight.Clicked += value; }
            remove { buttonEnterHeight.Clicked -= value; }
        }

        //            remove { buttonMale.Clicked -= value; }
        //        }

        public void GenderMaleButton_OnClicked(object sender, EventArgs e)
        {
            var s = (Button)sender;
            GenderOutput = s.StyleId;
        }
    }
}
