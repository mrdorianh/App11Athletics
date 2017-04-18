using Xamarin.Forms;

namespace App11Athletics.Views.Controls
{
    public class MyEntry : Entry
    {
        public MyEntry()
        {
            base.TextChanged += EditText;
            base.Focused += ClearText;
        }

        private void ClearText(object sender, FocusEventArgs e)
        {
            Entry d = sender as Entry;
            if (!string.IsNullOrEmpty(d?.Text))
                d.Text = string.Empty;
        }

        public void EditText(object sender, TextChangedEventArgs args)
        {
            Entry e = sender as Entry;
            var val = e?.Text;

            if (string.IsNullOrEmpty(val))
                return;

            if (MaxLength > 0 && val.Length > MaxLength)
            {
                val = val.Remove(val.Length - 1);
            }
            if (Uppercase)
                val = val.ToUpper();
            e.Text = val;
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