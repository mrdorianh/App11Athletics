using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToolkitXam
{
    public class NumericValidationAction : TriggerAction<Entry> 
    {
        protected override void Invoke(Entry entry)
        {
            double result;
            bool isValid = Double.TryParse(entry.Text, out result);
            entry.TextColor = isValid ? Color.Default : Color.Red;
        }
    }
}

