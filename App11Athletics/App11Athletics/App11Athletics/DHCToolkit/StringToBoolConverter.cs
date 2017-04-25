using System;
using System.Globalization;
using Xamarin.Forms;

namespace App11Athletics.DHCToolkit
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var valueAsString = (string)value;
            return !string.IsNullOrWhiteSpace(valueAsString) && valueAsString != "0";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
