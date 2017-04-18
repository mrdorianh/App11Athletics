using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Diagnostics;

using App11Athletics;
using Xamarin.Forms;
using Xamarin.Utilities.iOS;

using App11Athletics.DHCToolkit;
using App11Athletics.iOS;
using UIKit;
using Xamarin.Forms.Platform.iOS;
using XLabs.Forms.Behaviors;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(App11Athletics.DHCToolkit.ImageCircle), typeof(ImageCircleRenderer))]
namespace App11Athletics.iOS
{
    public class ImageCircleRenderer : ViewRenderer<DHCToolkit.ImageCircle, UIView>
    {

        private void CreateCircle()
        {
            try
            {
                double min = Math.Min(Element.Width, Element.Height);
                Control.Layer.CornerRadius = (float)(min / 2.0);
                Control.Layer.MasksToBounds = false;
                Control.Layer.BorderColor = Element.BorderColor.ToCGColor();

                Control.Layer.BorderWidth = Convert.ToSingle(Element.BorderThickness);
                Control.ClipsToBounds = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to create circle image: " + ex);
            }
        }

        #region Overrides of ImageRenderer

        #region Overrides of ViewRenderer<View,UIView>

        protected override void OnElementChanged(ElementChangedEventArgs<DHCToolkit.ImageCircle> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;

            CreateCircle();
        }

        #endregion

        #endregion

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == VisualElement.HeightProperty.PropertyName ||
                e.PropertyName == VisualElement.WidthProperty.PropertyName)
            {
                CreateCircle();
            }
        }

    }
}