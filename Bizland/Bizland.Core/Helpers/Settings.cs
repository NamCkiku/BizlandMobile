using Bizland.Core.Extensions;
using Bizland.Utilities.Constant;

using Plugin.Settings;
using Plugin.Settings.Abstractions;
using Xamarin.Forms;

namespace Bizland.Core
{
    public static class Settings
    {
        private static ISettings AppSettings => CrossSettings.Current;

        private const string IdLatitude = "latitude";
        private static readonly float LatitudeDefault = 20.973993f;

        private const string IdLongitude = "longitude";
        private static readonly float LongitudeDefault = 105.846421f;

        private const string UserNameKey = "user_name_key";
        private static readonly string UserNameDefault = string.Empty;

        private const string PasswordKey = "password_key";
        private static readonly string PasswordDefault = string.Empty;

        private const string RemembermeKey = "remember_me_key";
        private static readonly bool RemembermeDefault = false;

        private const string CurrentLanguageKey = "current_language_key";
        private static readonly string CurrentLanguageDefault = CultureCountry.Vietnamese;

        private const string FirebaseToken = "firebase_token";
        private static readonly string FirebaseTokenDefault = string.Empty;
        public static float Latitude
        {
            get => AppSettings.GetValueOrDefault(IdLatitude, LatitudeDefault);
            set => AppSettings.AddOrUpdateValue(IdLatitude, value);
        }

        public static float Longitude
        {
            get => AppSettings.GetValueOrDefault(IdLongitude, LongitudeDefault);
            set => AppSettings.AddOrUpdateValue(IdLongitude, value);
        }

        /// <summary>
        /// Lưu thông tin đăng nhập
        /// </summary>
        public static string UserName
        {
            get => AppSettings.GetValueOrDefault(UserNameKey, UserNameDefault);
            set => AppSettings.AddOrUpdateValue(UserNameKey, value);
        }

        /// <summary>
        /// Lưu thông tin mật khẩu
        /// </summary>
        public static string Password
        {
            get => AppSettings.GetValueOrDefault(PasswordKey, PasswordDefault);
            set => AppSettings.AddOrUpdateValue(PasswordKey, value);
        }

        /// <summary>
        /// Lưu thông tin ghi nho mat khau
        /// </summary>
        public static bool Rememberme
        {
            get => AppSettings.GetValueOrDefault(RemembermeKey, RemembermeDefault);
            set => AppSettings.AddOrUpdateValue(RemembermeKey, value);
        }

        /// <summary>
        /// ngôn ngữ hiện tại được chọn
        /// </summary>
        public static string CurrentLanguage
        {
            get => AppSettings.GetValueOrDefault(CurrentLanguageKey, CurrentLanguageDefault);
            set => AppSettings.AddOrUpdateValue(CurrentLanguageKey, value);
        }

        /// <summary>
        /// Lưu thông tin Token hiện tại của Firebase
        /// </summary>
        public static string CurrentFirebaseToken
        {
            get => AppSettings.GetValueOrDefault(FirebaseToken, FirebaseTokenDefault);
            set => AppSettings.AddOrUpdateValue(FirebaseToken, value);
        }
    }
}