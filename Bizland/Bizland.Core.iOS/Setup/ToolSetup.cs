using FFImageLoading.Forms.Platform;
using Syncfusion.XForms.iOS.Buttons;
using Xamarin.Forms.Platform.iOS;

namespace Bizland.Core.iOS.Setup
{
    public static class ToolSetup
    {
        public static FormsApplicationDelegate AppDelegate;

        public static void Initialize(FormsApplicationDelegate _AppDelegate)
        {
            AppDelegate = _AppDelegate;

            //Xamarin.Forms.DependencyService.Register<ToastNotification>();
            //ToastNotification.Init();
            //AnimationViewRenderer.Init();
            //CardsViewRenderer.Preserve();

            Rg.Plugins.Popup.Popup.Init();

            CachedImageRenderer.Init();
            FFImageLoading.FormsHandler.Init(debug: false);
            CachedImageRenderer.InitImageSourceHandler();

            // Override default ImageFactory by your implementation.
            //FormsGoogleMaps.Init(Config.GoogleMapKeyiOS, new PlatformConfig
            //{
            //    ImageFactory = new CachingImageFactory()
            //});

            // Syncfusion
            SfButtonRenderer.Init();
            //SfListViewRenderer.Init();
            //SfPickerRenderer.Init();
            //SfDataGridRenderer.Init();
            //SfCheckBoxRenderer.Init();
            //SfComboBoxRenderer.Init();
            //SfPullToRefreshRenderer.Init();
            //SfImageEditorRenderer.Init();
            //SfCalendarRenderer.Init();
            //SfBadgeViewRenderer.Init();
            //SfChartRenderer.Init();
            //SfBusyIndicatorRenderer.Init();
            //SfTabViewRenderer.Init();
            //SfRatingRenderer.Init();
            //SfPopupLayoutRenderer.Init();
            //SfMapsRenderer.Init();
        }
    }
}