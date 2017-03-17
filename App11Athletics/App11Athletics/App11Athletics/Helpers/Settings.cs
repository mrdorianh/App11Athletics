// Helpers/Settings.cs

using System;
using System.Collections.Generic;
using App11Athletics.Models;
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
        private static readonly string SettingsUserBmrDefault = string.Empty;

        private const string SettingsUserDce = "user_dce";
        private static readonly string SettingsUserDceDefault = string.Empty;

        private const string SettingsUserAge = "user_age";
        private static readonly string SettingsUserAgeDefault = "23";

        private const string SettingsUserHeightFt = "user_heightFt";
        private static readonly string SettingsUserHeightFtDefault = "5";

        private const string SettingsUserHeightIn = "user_heightIn";
        private static readonly string SettingsUserHeightInDefault = "9";

        private const string SettingsUserWeight = "user_weight";
        private static readonly string SettingsUserWeightDefault = string.Empty;

        private const string SettingsUserAlf = "user_alf";
        private static readonly double SettingsUserAlfDefault = 1.0;

        private const string SettingsUserAlfString = "user_alfString";
        private static readonly string SettingsUserAlfStringDefault = "Little to none";

        private const string SettingsUserEmail = "user_email";
        private static readonly string SettingsUserEmailDefault = string.Empty;

        private const string SettingsUserFamilyName = "user_family_name";
        private static readonly string SettingsUserFamilyNameDefault = string.Empty;

        private const string SettingsUserGender = "user_gender";
        private static readonly string SettingsUserGenderDefault = string.Empty;

        private const string SettingsUserGivenName = "user_given_name";
        private static readonly string SettingsUserGivenNameDefault = string.Empty;

        private const string SettingsUserPicture = "user_picture";
        private static readonly string SettingsUserPictureDefault = "icon.png";

        private const string SettingsUserRefreshToken = "user_refreshToken";
        private static readonly string SettingsUserRefreshTokenDefault = string.Empty;

        private const string SettingsUserName = "user_name";
        private static readonly string SettingsUserNameDefault = string.Empty;

        private const string SettingsEmailVerified = "user_email_verified";
        private static readonly bool SettingsEmailVerifiedDefault = false;

        private const string SettingsUserLocale = "user_locale";
        private static readonly string SettingsUserLocaleDefault = string.Empty;

        private const string SettingsUserUpdatedAt = "user_updated_at";
        private static readonly DateTime SettingsUserUpdatedAtDefault = DateTime.Now;

        private const string SettingsUserId = "user_id";
        private static readonly string SettingsUserIdDefault = string.Empty;

        private const string SettingsUserNickname = "user_nickname";
        private static readonly string SettingsUserNicknameDefault = string.Empty;

        private const string SettingsUserProvider = "user_provider";
        private static readonly string SettingsUserProviderDefault = string.Empty;

        private const string SettingsUserIdNumber = "user_id_number";
        private static readonly string SettingsUserIdNumberDefault = string.Empty;

        private const string SettingsUserConnection = "user_connection";
        private static readonly string SettingsUserConnectionDefault = string.Empty;

        private const string SettingsUserIsSocial = "user_provider";
        private static readonly bool SettingsUserIsSocialDefault = false;

        private const string SettingsOneRMLift = "user_OneRMLift";
        private static readonly string SettingsOneRMLiftDefault = "ADD EXERCISE";

        private const string SettingsOneRMWeight = "user_OneRMWeight";
        private static readonly string SettingsOneRMWeightDefault = "0";
        private const string SettingsOneRMAx = "user_OneRMax";
        private static readonly string SettingsOneRMaxDefault = "0";

        public static string UserOneRMLift
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsOneRMLift, SettingsOneRMLiftDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsOneRMLift, value);
            }
        }

        public static string UserOneRMWeight
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsOneRMWeight, SettingsOneRMWeightDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsOneRMWeight, value);
            }
        }
        public static string UserOneRMAx
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsOneRMAx, SettingsOneRMaxDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsOneRMAx, value);
            }
        }

        public static string UserBmr
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserBmr, SettingsUserBmrDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserBmr, value);
            }
        }

        public static string UserDce
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserDce, SettingsUserDceDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserDce, value);
            }
        }
        public static string UserAge
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserAge, SettingsUserAgeDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserAge, value);
            }
        }
        public static string UserHeightFt
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserHeightFt, SettingsUserHeightFtDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserHeightFt, value);
            }
        }
        public static string UserHeightIn
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserHeightIn, SettingsUserHeightInDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserHeightIn, value);
            }
        }
        public static string UserWeight
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserWeight, SettingsUserWeightDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserWeight, value);
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
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserRefreshToken, SettingsUserRefreshTokenDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserRefreshToken, value);
            }
        }

        public static bool EmailVerified
        {
            get { return AppSettings.GetValueOrDefault<bool>(SettingsEmailVerified, SettingsEmailVerifiedDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(SettingsEmailVerified, value);
            }
        }

        public static string UserName
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserName, SettingsUserNameDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserName, value);
            }
        }

        public static string UserLocale
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserLocale, SettingsUserLocaleDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserLocale, value);
            }
        }

        public static DateTime UserUpdatedAt
        {
            get { return AppSettings.GetValueOrDefault<DateTime>(SettingsUserUpdatedAt, SettingsUserUpdatedAtDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<DateTime>(SettingsUserUpdatedAt, value);
            }
        }

        public static string UserId
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserId, SettingsUserIdDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserId, value);
            }
        }

        public static string UserNickname
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserNickname, SettingsUserNicknameDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserNickname, value);
            }
        }

        public static string UserProvider
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserProvider, SettingsUserProviderDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserProvider, value);
            }
        }
        public static string UserIdNumber
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserIdNumber, SettingsUserIdNumberDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserIdNumber, value);
            }
        }
        public static string UserConnection
        {
            get { return AppSettings.GetValueOrDefault<string>(SettingsUserConnection, SettingsUserConnectionDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<string>(SettingsUserConnection, value);
            }
        }
        public static bool UserIsSocial
        {
            get { return AppSettings.GetValueOrDefault<bool>(SettingsUserIsSocial, SettingsUserIsSocialDefault); }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(SettingsUserIsSocial, value);
            }
        }
    }
}