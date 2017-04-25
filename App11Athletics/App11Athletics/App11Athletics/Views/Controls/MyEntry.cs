using System;
using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    public class MyEntry : Entry
    {
        public MyEntry()
        {
            base.TextChanged += EditText;
            base.Focused += ClearText;
            base.Unfocused += OnUnfocused;
        }

        void OnUnfocused(object sender, FocusEventArgs focusEventArgs)
        {
            var e = sender as Entry;
            if (!Uppercase)
                return;
            if (e != null)
                e.Text = e.Text.ToUpper();
        }

        private static void ClearText(object sender, FocusEventArgs e)
        {
            var d = sender as Entry;
            if (!string.IsNullOrEmpty(d?.Text))
                d.Text = string.Empty;
        }

        public void EditText(object sender, TextChangedEventArgs args)
        {
            var e = sender as Entry;
            string val = null;
            if (e != null)
                val = ((Entry)sender).Text;
            if (string.IsNullOrEmpty(val))
                return;
            if ((MaxLength <= 0) || (val.Length <= MaxLength))
                return;

            e.Text = val.Remove(val.Length - 1);
        }

        public static readonly BindableProperty UppercaseProperty =
            BindableProperty.Create<MyEntry, bool>(p => p.Uppercase, false);

        public bool Uppercase
        {
            get { return (bool)GetValue(UppercaseProperty); }
            set { SetValue(UppercaseProperty, value); }
        }

        public static readonly BindableProperty MaxLengthProperty =
            BindableProperty.Create<MyEntry, int>(p => p.MaxLength, 0);

        public int MaxLength
        {
            get { return (int)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }
    }
}