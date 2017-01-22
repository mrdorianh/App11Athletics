using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App11Athletics.Helpers;
using Newtonsoft.Json;
using PropertyChanged;

namespace App11Athletics.Models
{
    public class UserProfileModel : INotifyPropertyChanged
    {
        public UserProfileModel() { }

        public string Email
        {
            get { return Settings.UserEmail; }
            set { Settings.UserEmail = value; }
        }

        public bool EmailVerified
        {
            get { return Settings.EmailVerified; }
            set { Settings.EmailVerified = value; }
        }

        public string Name => Settings.UserName;

        public string GivenName
        {
            get { return Settings.UserGivenName; }
            set { Settings.UserGivenName = value; }
        }

        public string FamilyName
        {
            get { return Settings.UserFamilyName; }
            set { Settings.UserFamilyName = value; }
        }

        public string Picture
        {
            get { return Settings.UserPicture; }
            set { Settings.UserPicture = value; }
        }

        public string Gender
        {
            get { return Settings.UserGender; }
            set { Settings.UserGender = value; }
        }

        public string GenderSymbol
        {
            get
            {
                if (Settings.UserGender.ToLower() == "male")
                    return "M";
                else if (Settings.UserGender.ToLower() == "female")
                {
                    return "F";
                }
                else
                {
                    return string.Empty;
                }
            }
            set { }
        }

        public string Locale
        {
            get { return Settings.UserLocale; }
            set { Settings.UserLocale = value; }
        }

        public string Age
        {
            get { return Settings.UserAge; }
            set { Settings.UserAge = value; }
        }

        public string Weight
        {
            get { return Settings.UserWeight; }
            set { Settings.UserWeight = value; }
        }

        public static DateTime UpdatedAt
        {
            get { return Settings.UserUpdatedAt; }
            set { Settings.UserUpdatedAt = value; }
        }

        public static string UserId
        {
            get { return Settings.UserId; }
            set { Settings.UserId = value; }
        }

        public static string Nickname
        {
            get { return Settings.UserNickname; }
            set { Settings.UserNickname = value; }
        }

        public string HeightFt
        {
            get { return Settings.UserHeightFt; }
            set { Settings.UserHeightFt = value; }
        }
        public string HeightIn
        {
            get { return Settings.UserHeightIn; }
            set { Settings.UserHeightIn = value; }
        }

        public string ActivityLevel
        {
            get { return Settings.UserAlfString; }
            set { Settings.UserAlfString = value; }
        }

        public string Bmr
        {
            get { return Settings.UserBmr; }
            set { Settings.UserBmr = value; }
        }
        public string Dce
        {
            get { return Settings.UserDce; }
            set { Settings.UserDce = value; }
        }

        #region Implementation of INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}