using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ToolkitXam
{
    public class CountedLabel : Label
    {
        static readonly BindablePropertyKey WordCountKey =
            BindableProperty.CreateReadOnly("WordCount",            // propertyName
                                            typeof(int),            // returnType
                                            typeof(CountedLabel),   // declaringType
                                            0);                     // defaultValue

        public static readonly BindableProperty WordCountProperty = WordCountKey.BindableProperty;

        public CountedLabel()
        {
            // Set the WordCount property when the Text property changes.
            PropertyChanged += (object sender, PropertyChangedEventArgs args) =>
                {
                    if (args.PropertyName == "Text")
                    {
                        if (String.IsNullOrEmpty(Text))
                        {
                            WordCount = 0;
                        }
                        else
                        {
                            WordCount = Text.Split(' ', '-', '\u2014').Length;
                        }
                    }
                };
        }

        public int WordCount
        {
            private set { SetValue(WordCountKey, value); }
            get { return (int)GetValue(WordCountProperty); }
        }
    }
}
