using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11Athletics.DHCToolkit
{
    public class AnimatePages
    {
        public static async Task AnimatePageIn(Grid grid)
        {
            var h = 1500;
            var t = 250;
            var tu = 450U;
            foreach (var view in grid.Children)
            {

                view.TranslationY = h;
            }
            await Task.Delay(300);
            foreach (var view in grid.Children)
            {
                await Task.WhenAny(view.TranslateTo(0, 0, tu, Easing.CubicOut),
                    view.ScaleTo(1, tu, Easing.SinInOut), Task.Delay(t));

                tu = tu + 75U;
            }
        }
        public static async Task AnimatePageIn(StackLayout stackLayout)
        {
            var h = 1500;
            var t = 250;
            var tu = 450U;
            foreach (var view in stackLayout.Children)
            {

                view.TranslationY = h;
            }
            await Task.Delay(300);
            foreach (var view in stackLayout.Children)
            {
                await Task.WhenAny(view.TranslateTo(0, 0, tu, Easing.CubicOut),
                    view.ScaleTo(1, tu, Easing.SinInOut), Task.Delay(t));

                tu = tu + 75U;
            }
        }

        public static async Task AnimatePageOut(Grid grid)
        {
            var h = 1500;
            var t = 250;
            var tu = 450U;
            foreach (var view in grid.Children)
            {
                view.TranslationY = 0;
            }
            foreach (var view in grid.Children)
            {
                await Task.WhenAny(view.TranslateTo(0, h, tu, Easing.CubicIn),
                    view.ScaleTo(0.7, tu, Easing.CubicIn), Task.Delay(t));
            }
        }

        public static async Task AnimatePageOut(StackLayout stackLayout)
        {
            var h = 1500;
            var t = 250;
            var tu = 450U;
            foreach (var view in stackLayout.Children)
            {
                view.TranslationY = 0;
            }
            foreach (var view in stackLayout.Children)
            {
                await Task.WhenAny(view.TranslateTo(0, h, tu, Easing.CubicIn),
                    view.ScaleTo(0.7, tu, Easing.CubicIn), Task.Delay(t));
            }
        }


    }
}
