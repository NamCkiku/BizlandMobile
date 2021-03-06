﻿using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

using System.Threading.Tasks;

using Xamarin.Forms;

namespace Bizland.Core
{
    /// <summary>
    /// Class làm việc với quyền
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// Namth  11/27/2017   created
    /// </Modified>
    public static class PermissionHelper
    {
        private const string POSITIVE = "Cài đặt";
        private const string NEGATIVE = "Để sau";

        /// <summary>
        /// Kiểm tra xem có quyền không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Namth  27/11/2017   created
        /// </Modified>
        public static async Task<bool> CheckLocationPermissions(bool isAleart = true)
        {
            var title = "Quyền truy cập vị trí";
            var question = "Chức năng yêu cầu quyền truy cập vị trí của bạn.";

            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationPermission>();

            // Chưa bật định vị
            if (permissionStatus == PermissionStatus.Granted)
            {
                var locator = CrossGeolocator.Current;

                var isGeolocationEnabled = locator.IsGeolocationEnabled;

                if (!isGeolocationEnabled)
                {
                    if (isAleart)
                    {
                        var task = Application.Current?.MainPage?.DisplayAlert(title, question, POSITIVE, NEGATIVE);
                        if (task == null)
                            return false;

                        var result = await task;

                        if (result)
                        {
                            DependencyService.Get<ISettingsService>().OpenLocationSettings();
                        }
                    }

                    return false;
                }

                return true;
            }
            else
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                {
                    if (isAleart)
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            var task = Application.Current?.MainPage?.DisplayAlert(title, question, POSITIVE, NEGATIVE);
                            if (task == null)
                                return false;

                            var result = await task;
                            if (result)
                            {
                                CrossPermissions.Current.OpenAppSettings();
                            }

                            return false;
                        }

                        permissionStatus = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();

                        if (permissionStatus == PermissionStatus.Granted)
                        {
                            return true;
                        }
                        else
                        {
                            var task = Application.Current?.MainPage?.DisplayAlert(title, question, POSITIVE, NEGATIVE);
                            if (task == null)
                                return false;

                            var result = await task;
                            if (result)
                            {
                                CrossPermissions.Current.OpenAppSettings();
                            }

                            return false;
                        }
                    }
                }
                else
                {
                    permissionStatus = await CrossPermissions.Current.RequestPermissionAsync<LocationPermission>();

                    if (permissionStatus == PermissionStatus.Granted)
                    {
                        return true;
                    }
                    else
                    {
                        if (isAleart)
                        {
                            var task = Application.Current?.MainPage?.DisplayAlert(title, question, POSITIVE, NEGATIVE);
                            if (task == null)
                                return false;

                            var result = await task;
                            if (result)
                            {
                                CrossPermissions.Current.OpenAppSettings();
                            }
                            return false;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Kiểm tra xem có quyền truy cập Camera không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckCameraPermissions()
        {
            var title = "Quyền truy cập camera";
            var question = "Chức năng yêu cầu quyền truy cập camera của bạn.";

            return await CheckPermission<CameraPermission>(Permission.Camera, title, question);
        }

        /// <summary>
        /// Kiểm tra xem có quyền truy cập thư mục ảnh không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckPhotoPermissions()
        {
            var title = "Quyền truy cập thư mục ảnh";
            var question = "Chức năng yêu cầu quyền truy cập thư mục ảnh của bạn.";

            return await CheckPermission<PhotosPermission>(Permission.Photos, title, question);
        }

        /// <summary>
        /// Kiểm tra xem có quyền truy cập thư mục ảnh không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckStoragePermissions()
        {
            var title = "Quyền truy cập bộ nhớ";
            var question = "Chức năng yêu cầu quyền truy cập bộ nhớ của bạn.";

            return await CheckPermission<StoragePermission>(Permission.Storage, title, question);
        }

        /// <summary>
        /// Kiểm tra xem có quyền không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckPermission<TPermission>(Permission permission, string title, string question) where TPermission : BasePermission, new()
        {
            return await CheckPermission<TPermission>(permission, title, question, POSITIVE, NEGATIVE);
        }

        /// <summary>
        /// Kiểm tra xem có quyền không thì mới tiếp tục cho phép hoạt động.
        /// </summary>
        /// <param name="permission">The permission.</param>
        /// <returns></returns>
        /// <Modified>
        /// Name     Date         Comments
        /// Truongpv  09/08/2018   created
        /// </Modified>
        public static async Task<bool> CheckPermission<TPermission>(Permission permission, string title, string question, string positive, string negative) where TPermission : BasePermission, new()
        {
            bool request = false;

            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync<TPermission>();

            if (permissionStatus == PermissionStatus.Denied)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                {
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                        if (task == null)
                            return false;

                        var result = await task;
                        if (result)
                        {
                            CrossPermissions.Current.OpenAppSettings();
                        }

                        return false;
                    }
                }

                request = true;
            }

            if (request || permissionStatus != PermissionStatus.Granted)
            {
                permissionStatus = await CrossPermissions.Current.RequestPermissionAsync<TPermission>();

                if (permissionStatus != PermissionStatus.Granted)
                {
                    var task = Application.Current?.MainPage?.DisplayAlert(title, question, positive, negative);
                    if (task == null)
                        return false;

                    var result = await task;
                    if (result)
                    {
                        CrossPermissions.Current.OpenAppSettings();
                    }

                    return false;
                }
            }

            return true;
        }
    }
}