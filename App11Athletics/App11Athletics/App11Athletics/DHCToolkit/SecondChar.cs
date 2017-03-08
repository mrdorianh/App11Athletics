using System;
using System.Globalization;
using Xamarin.Forms;

namespace App11Athletics.DHCToolkit
{
    public class SecondChar : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var str = (TimeSpan)value;
            string time = null;
            switch (parameter.ToString())
            {
                case "ff":
                    time = str.ToString("ff");
                    break;
                case "ss":
                    time = str.ToString("ss");
                    break;
                case "mm":
                    time = str.ToString("mm");
                    break;
                case "hh":
                    time = str.ToString("hh");
                    break;
            }
            //            var second = str?.Substring(1, 1);
            return time?.Substring(1, 1);
            ;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
