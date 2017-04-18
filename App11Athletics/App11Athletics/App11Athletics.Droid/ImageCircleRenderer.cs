using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using App11Athletics.Droid;
using App11Athletics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XLabs.Forms.Controls;
using App11Athletics.DHCToolkit;
using Color = Android.Graphics.Color;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(App11Athletics.DHCToolkit.ImageCircle), typeof(ImageCircleRenderer))]
namespace App11Athletics.Droid
{
    public class ImageCircleRenderer : ViewRenderer<DHCToolkit.ImageCircle, GridLayout>
    {

        protected override bool DrawChild(Canvas canvas, global::Android.Views.View child, long drawingTime)
        {
            try
            {
                var radius = Math.Min(Width, Height) / 2;
                var strokeWidth = 10;
                radius -= strokeWidth / 2;


                //Create path to clip
                var path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);

                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();
                // Create path for circle border
                path = new Path();
                path.AddCircle(Width / 2, Height / 2, radius, Path.Direction.Ccw);

                var paint = new Paint();
                paint.AntiAlias = true;
                paint.StrokeWidth = Convert.ToSingle(Element.BorderThickness);
                paint.SetStyle(Paint.Style.Stroke);
                //                paint.Color = global::Android.Graphics.Color.Black;
                paint.Color = Element.BorderColor.ToAndroid();
                canvas.DrawPath(path, paint);

                //Properly dispose
                paint.Dispose();
                path.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Unable to create circle image: " + ex);
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
        protected override void OnElementChanged(ElementChangedEventArgs<DHCToolkit.ImageCircle> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                if ((int)Android.OS.Build.VERSION.SdkInt < 18)
                    SetLayerType(LayerType.Software, null);
            }
            if (e.OldElement != null || this.Element == null)
                return;
//            Element.PropertyChanged += (sender, args) => Invalidate();


        }

    }
}