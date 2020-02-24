using Bizland.Core.iOS.Factories;
using Bizland.Utilities.Constant;
using FFImageLoading.Forms.Platform;
using Plugin.GoogleClient;
using Sharpnado.Presentation.Forms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.EffectsView;
using Xamarin.Forms.Platform.iOS;

namespace Bizland.Core.iOS.Setup
{
    public static class ToolSetup
    {
        public static FormsApplicationDelegate AppDelegate;

        public static void Initialize(FormsApplicationDelegate _AppDelegate)
        {
            AppDelegate = _AppDelegate;

            GoogleClientManager.Initialize();


            //Xamarin.Forms.DependencyService.Register<ToastNotification>();
            //ToastNotification.Init();

            CachedImageRenderer.Init();
            FFImageLoading.FormsHandler.Init(debug: false);
            CachedImageRenderer.InitImageSourceHandler();

            // Override default ImageFactory by your implementation.
            FormsGoogleMaps.Init(Config.GoogleMapKeyiOS, new PlatformConfig
            {
                ImageFactory = new CachingImageFactory()
            });
            SharpnadoInitializer.Initialize();
            // Syncfusion
            SfButtonRenderer.Init();
            SfRotatorRenderer.Init();
            SfEffectsViewRenderer.Init();
            SfSegmentedControlRenderer.Init();
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