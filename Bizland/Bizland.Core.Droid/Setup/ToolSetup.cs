using Android.App;
using Android.OS;
using Bizland.Core.Droid.DependencyService;
using FFImageLoading.Forms.Platform;
using Plugin.AppShortcuts;
using Plugin.GoogleClient;
using Sharpnado.Presentation.Forms.Droid;

namespace Bizland.Core.Droid.Setup
{
    public static class ToolSetup
    {
        public static void Initialize(Activity activity, Bundle bundle)
        {
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(activity, bundle);

            CrossAppShortcuts.Current.Init();

            SharpnadoInitializer.Initialize();

            GoogleClientManager.Initialize(activity);

            //Xamarin.Forms.DependencyService.Register<ToastNotification>();

            //ToastNotification.Init(activity, new PlatformOptions() { SmallIconDrawable = Android.Resource.Drawable.IcDialogInfo });

            Xamarin.Essentials.Platform.Init(activity, bundle); // add this line to your code, it may also be called: bundle

            //Acr.UserDialogs.UserDialogs.Init(activity);

            CachedImageRenderer.Init(enableFastRenderer: true);
            CachedImageRenderer.InitImageViewHandler();
            //This forces the custom renderers to be used
            //Android.Glide.Forms.Init(activity, debug: false);

            // Override default BitmapDescriptorFactory by your implementation
            FormsGoogleMaps.Init(activity, bundle, new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            });
        }
    }
}