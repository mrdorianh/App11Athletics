using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
[assembly: ExportRenderer(typeof(Button), typeof(App11Athletics.Droid.FlatButtonRenderer))]
namespace App11Athletics.Droid
{
    public class FlatButtonRenderer : ButtonRenderer
    {
        protected override void OnDraw(Android.Graphics.Canvas canvas)
        {
            base.OnDraw(canvas);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
        }
    }
}