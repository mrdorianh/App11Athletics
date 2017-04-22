using System.Threading.Tasks;
using Xamarin.Forms;

namespace App11Athletics.DHCToolkit
{
    public class AnimatePages
    {
        public static async Task AnimatePageIn(Grid grid)
        {
            var h = 1500;
            var t = 150;
            var tu = 250U;
            var children = 1U;

            foreach (var view in grid.Children)
            {
                view.TranslationY = h;

            }
            await Task.Delay(50);
            foreach (var view in grid.Children)
            {
                await Task.WhenAny(view.TranslateTo(0, 0, tu, Easing.CubicOut),
                    view.FadeTo(1, tu, Easing.SinInOut), Task.Delay(t));
                tu = tu + 5U;
                t = t + 5;
            }
        }
        public static async Task AnimatePageIn(StackLayout stackLayout)
        {
            var h = 1500;
            var t = 250;
            var tu = 250U;


            foreach (var view in stackLayout.Children)
            {
                view.TranslationY = h;
            }
            await Task.Delay(50);
            foreach (var view in stackLayout.Children)
            {
                await Task.WhenAny(view.TranslateTo(0, 0, tu, Easing.CubicOut),
                    view.FadeTo(1, tu, Easing.SinInOut), Task.Delay(t));

                tu = tu + 5U;
            }
        }

        public static async Task BgLogoTask(Image image, double x = 0, double y = 0)
        {
            image.Rotation = 0;
            image.RotateTo(360, 1200U, Easing.SinOut);

            await image.TranslateTo(x, y, 1200U, Easing.CubicInOut);
            image.Rotation = 0;
        }
        public static async Task BgLogoTaskOut(Image image, double x = 0, double y = 0)
        {
            image.Rotation = 0;
            image.RotateTo(-360, 1200U, Easing.SinOut);

            await image.TranslateTo(x, y, 1200U, Easing.CubicInOut);
            image.Rotation = 0;
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
                await Task.WhenAny(view.TranslateTo(0, h, tu, Easing.SinIn),
                      Task.Delay(t));
            }
            await Task.Delay(250);
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

                await Task.WhenAny(view.TranslateTo(0, h, tu, Easing.SinIn),
                      Task.Delay(t));
            }
            await Task.Delay(450);
        }


    }
}
