// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace App11Athletics.Helpers
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Settings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        //    private const string SettingsKey = "settings_key";
        //    private static readonly string SettingsDefault = string.Empty;

        #endregion


        //    public static string GeneralSettings
        //    {
        //      get
        //      {
        //        return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);
        //      }
        //      set
        //      {
        //        AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
        //      }
        //    }
        private const string SettingsUserBmr = "user_bmr";
        private static readonly double SettingsUserBmrDefault = 0.0;

        //
        private const string SettingsUserDce = "user_dce";
        private static readonly double SettingsUserDceDefault = 0.0;

        //
        private const string SettingsUserAge = "user_age";
        private static readonly double SettingsUserAgeDefault = 20;

        //
        private const string SettingsUserHeightFt = "user_heightFt";
        private static readonly double SettingsUserHeightFtDefault = 5;

        //
        private const string SettingsUserHeightIn = "user_heightIn";
        private static readonly double SettingsUserHeightInDefault = 10;

        private const string SettingsUserWeight = "user_weight";
        private static readonly double SettingsUserWeightDefault = 195;

        private const string SettingsUserAlf = "user_alf";
        private static readonly double SettingsUserAlfDefault = 1.0;

        private const string SettingsUserAlfString = "user_alfString";
        private static readonly string SettingsUserAlfStringDefault = "Unspecified";

        //
        private const string SettingsUserEmail = "user_email";
        private static readonly string SettingsUserEmailDefault = string.Empty;

        //
        private const string SettingsUserFamilyName = "user_family_name";
        private static readonly string SettingsUserFamilyNameDefault = string.Empty;

        //
        private const string SettingsUserGender = "user_gender";
        private static readonly string SettingsUserGenderDefault = "Male";

        //
        private const string SettingsUserGivenName = "user_given_name";
        private static readonly string SettingsUserGivenNameDefault = string.Empty;

        //
        private const string SettingsUserPicture = "user_picture";
        private static readonly string SettingsUserPictureDefault = "icon.png";


        private const string SettingsUserRefreshToken = "user_refreshToken";
        private static readonly string SettingsUserRefreshTokenDefault = string.Empty;







        //public static string GeneralSettings
        //{
        //  get { return AppSettings.GetValueOrDefault<string>(SettingsKey, SettingsDefault);}
        //  set{AppSettings.AddOrUpdateValue<string>(SettingsKey, value);
        //  }
        //}
        public static double UserBmr
        {
            get { return AppSettings.GetValueOrDefault<double>(SettingsUserBmr, SettingsUserBmrDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<double>(SettingsUserBmr, value);
            }
        }

        public static double UserDce
        {
            get { return AppSettings.GetValueOrDefault<double>(SettingsUserDce, SettingsUserDceDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<double>(SettingsUserDce, value);
            }
        }
        public static double UserAge
        {
            get { return AppSettings.GetValueOrDefault<double>(SettingsUserAge, SettingsUserAgeDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<double>(SettingsUserAge, value);
            }
        }
        public static double UserHeightFt
        {
            get { return AppSettings.GetValueOrDefault<double>(SettingsUserHeightFt, SettingsUserHeightFtDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<double>(SettingsUserHeightFt, value);
            }
        }
        public static double UserHeightIn
        {
            get { return AppSettings.GetValueOrDefault<double>(SettingsUserHeightIn, SettingsUserHeightInDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<double>(SettingsUserHeightIn, value);
            }
        }
        public static double UserWeight
        {
            get { return AppSettings.GetValueOrDefault<double>(SettingsUserWeight, SettingsUserWeightDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<double>(SettingsUserWeight, value);
            }
        }

        public static double UserAlf
        {
            get { return AppSettings.GetValueOrDefault<double>(SettingsUserAlf, SettingsUserAlfDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<double>(SettingsUserAlf, value);
            }
        }
        public static string UserAlfString
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserAlfString, SettingsUserAlfStringDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserAlfString, value);
            }
        }
        public static string UserEmail
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserEmail, SettingsUserEmailDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserEmail, value);
            }
        }

        public static string UserGender
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserGender, SettingsUserGenderDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserGender, value);
            }
        }

        public static string UserFamilyName
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserFamilyName, SettingsUserFamilyNameDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserFamilyName, value);
            }
        }
        public static string UserGivenName
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserGivenName, SettingsUserGivenNameDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserGivenName, value);
            }
        }
        public static string UserPicture
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserPicture, SettingsUserPictureDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserPicture, value);
            }
        }
        public static string UserRefreshToken
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserRefreshToken, SettingsUserRefreshToken); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserRefreshToken, value);
            }
        }
    }
}