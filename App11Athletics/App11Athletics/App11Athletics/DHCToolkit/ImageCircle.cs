using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Helpers;
using App11Athletics.Views.Controls;
using ExifLib;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace App11Athletics.DHCToolkit
{

    public class ImageCircle : Grid
    {
        public ImageCircle()
        {
            Padding = 0;
            Margin = 5;
            var CircleContentImage = new Image() { Aspect = Aspect.AspectFill, BindingContext = this, VerticalOptions = LayoutOptions.FillAndExpand };
            CircleContentImage.SetBinding(Image.RotationProperty, "ImageRotation");
            CircleContentImage.SetBinding(Image.SourceProperty, "MediaSource");
            Children.Add(CircleContentImage);

        }

        #region Overrides of Element
        //        public void UpdateImageRotation()
        //        {
        //
        //            switch (SourceOrientation)
        //            {
        //                case ExifOrientation.Undefined:
        //                    {
        //                        ImageRotation = 0.0;
        //                    }
        //                    break;
        //                case ExifOrientation.TopLeft:
        //                    {
        //                        ImageRotation = 0.0;
        //                    }
        //                    break;
        //                case ExifOrientation.TopRight:
        //                    {
        //                        ImageRotation = 90.0;
        //                    }
        //                    break;
        //                case ExifOrientation.BottomLeft:
        //                    {
        //                        ImageRotation = -90.0;
        //                    }
        //                    break;
        //                case ExifOrientation.BottomRight:
        //                    {
        //                        ImageRotation = 180.0;
        //                    }
        //                    break;
        //                default:
        //                    ImageRotation = 0.0;
        //                    break;
        //            }
        //            Settings.UserProfileImageRotation = ImageRotation;
        //        }

        #endregion

        #region Overrides of Element



        #endregion

        //        public Image CircleContentImage;
        //        public static readonly BindableProperty MediaSourceProperty =
        //           BindableProperty.Create<ImageCircle, ImageSource>(p => p.MediaSource, Settings.UserPicture);

        //        public ImageSource MediaSource
        //        {
        //            get { return (ImageSource)GetValue(MediaSourceProperty); }
        //            set { SetValue(MediaSourceProperty, value); }
        //        }
        //        public static readonly BindableProperty SourceOrientationProperty =
        //          BindableProperty.Create<ImageCircle, ExifOrientation>(p => p.SourceOrientation, ExifOrientation.Undefined);


        //        public ExifOrientation SourceOrientation
        //        {
        //            get { return (ExifOrientation)GetValue(SourceOrientationProperty); }
        //            set { SetValue(SourceOrientationProperty, value); }
        //        }
        public static readonly BindableProperty BorderColorProperty =
          BindableProperty.Create("BorderColor", typeof(Color), typeof(ImageCircle), (Color)Application.Current.Resources["ColorBrandGlobalBlue"]);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        //        public static readonly BindableProperty ImageRotationProperty =
        //            BindableProperty.Create("ImageRotation", typeof(double), typeof(ImageCircle), Settings.UserProfileImageRotation);
        //
        //
        //        public double ImageRotation
        //        {
        //            get { return (double)GetValue(ImageRotationProperty); }
        //            set
        //            {
        //                SetValue(ImageRotationProperty, value);
        //            }
        //        }

        public static readonly BindableProperty BorderThicknessProperty =
                   BindableProperty.Create("BorderThickness", typeof(int), typeof(ImageCircle), 8);


        public int BorderThickness
        {
            get { return (int)GetValue(BorderThicknessProperty); }
            set
            {
                SetValue(BorderThicknessProperty, value);
            }
        }

    }
}
