using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToolkitXam
{
    public class AspectRatioTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFrom(CultureInfo culture, object value)
        {
            string str = value as string;

            if (String.IsNullOrWhiteSpace(str))
                return null;

            str = str.Trim();
            double aspectValue;

            if (String.Compare(str, "auto", StringComparison.OrdinalIgnoreCase) == 0)
                return AspectRatio.Auto;

            if (Double.TryParse(str, NumberStyles.Number, 
                                CultureInfo.InvariantCulture, out aspectValue))
                return new AspectRatio(aspectValue);

            throw new FormatException("AspectRatio must be Auto or numeric");
        }
    }
}
