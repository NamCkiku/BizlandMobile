using Bizland.Core.Resource;

using Plugin.Media;

using System.Threading.Tasks;

using Xamarin.Forms;

namespace Bizland.Core.Helpers
{
    public class PhotoHelper
    {
        public static async Task<bool> CanTakePhoto()
        {
            if (!await PermissionHelper.CheckCameraPermissions() || !await PermissionHelper.CheckStoragePermissions())
                return false;

            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable)
            {
                await Application.Current.MainPage.DisplayAlert(MobileResource.Common_Label_CameraTitle, MobileResource.Common_Message_CameraUnavailable, MobileResource.Common_Button_OK);
                return false;
            }

            if (!CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert(MobileResource.Common_Label_CameraTitle, MobileResource.Common_Message_CameraUnsupported, MobileResource.Common_Button_OK);
                return false;
            }

            return true;
        }

        public static async Task<bool> CanPickPhoto()
        {
            if (!await PermissionHelper.CheckPhotoPermissions() || !await PermissionHelper.CheckStoragePermissions())
                return false;

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert(MobileResource.Common_Message_PickPhotoUnsupported, MobileResource.Common_Message_PickPhotoPermissionNotGranted, MobileResource.Common_Button_OK);
                return false;
            }

            return true;
        }
    }
}