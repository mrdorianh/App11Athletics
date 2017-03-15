using System;
using System.Globalization;
using Xamarin.Forms;

namespace App11Athletics.DHCToolkit
{
    public class CaseToUpper : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;

            var upper = str?.ToUpper();
            return upper;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
