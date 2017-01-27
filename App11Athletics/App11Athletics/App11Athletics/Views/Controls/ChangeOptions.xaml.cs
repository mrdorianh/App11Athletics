using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public void Entry_OnUnfocused(object sender, FocusEventArgs e)
        {

        }

        public new bool Focus()
        {
            return entry.Focus();
        }

        public new void Unfocus()
        {
            entry.Unfocus();
        }


        public event EventHandler<FocusEventArgs> Donefocused
        {
            add { entry.Unfocused += value; }
            remove { entry.Unfocused -= value; }
        }

        private void Entry_OnCompleted(object sender, EventArgs e)
        {
            OptionsOutput = entry.Text;
        }

        private void ChangeOptions_OnSizeChanged(object sender, EventArgs e)
        {
            OptionsFontSize = Width / 6;
            OptionsPlaceHolderFontSize = Width / 8;
        }
    }
}
