using App11Athletics.Helpers;
using Xamarin.Forms;

namespace App11Athletics.Models
{

    public class CarouselModel
    {
        public CarouselModel(string imagestr, bool isProfileImage = false)
        {
            IsProfile = isProfileImage;
            Image = imagestr;
            if (isProfileImage)
            {
                ImageRotation = Settings.UserProfileImageRotation;
            }

        }

        private string _image;

        public string Image
        {
            get { return _image; }
            set { _image = value; }
        }

        public double ImageRotation
        {
            get { return 0.0; }
            set { }
        }

        public bool NotProfile => !IsProfile;

        public bool IsProfile { get; set; }


    }
}