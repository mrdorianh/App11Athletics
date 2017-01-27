using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11Athletics.Views
{
    public class AnimateInOrOut
    {

        public AnimateInOrOut()
        {
        }

        public static async Task AnimateInOrOutPage<T>(Layout<T> layout, VisualElement parent, bool In = false)
            where T : View
        {
            // true = In       // false = out 

            var h = parent.Height * 1.5;
            foreach (View view in layout.Children)
            {
                view.TranslationY = h;
            }
            if (!In)
                foreach (View view in layout.Children)
                {
                    await Task.WhenAny(view.TranslateTo(0, h, 500U, Easing.CubicInOut),
                        view.ScaleTo(0.8, 500U, Easing.SinInOut), Task.Delay(250));
                }
            else
            {
                foreach (View view in layout.Children)
                {
                    await Task.WhenAny(view.TranslateTo(0, 0, 500U, Easing.CubicInOut),
                        view.ScaleTo(1, 500U, Easing.SinInOut), Task.Delay(250));
                }
            }
        }
    }
}