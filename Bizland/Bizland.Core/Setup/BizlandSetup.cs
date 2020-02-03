using Bizland.Core.ViewModels;
using Bizland.Core.Views;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Plugin.Popups;

using Xamarin.Forms;

namespace Bizland.Core.Setup
{
    public static class BizlandSetup
    {
        public static void Initialize()
        {
            //AssetsExtension.InitAssetsExtension("AppResources.Assets", typeof(App).GetTypeInfo().Assembly);
            //ImageResourceExtension.InitImageResourceExtension("AppResources.Assets", typeof(App).GetTypeInfo().Assembly);

            //Đăng kí sử dụng Syncfusion
            //Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Config.SyncfusionKey);

            //CultureHelper.SetCulture();

            // Đăng ký config automapper
            //AutoMapperConfig.Initialize();
        }

        public static void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<INavigationService, PopupPageNavigationService>();

            // This updates INavigationService and registers PopupNavigation.Instance
            containerRegistry.RegisterPopupNavigationService();

            //containerRegistry.RegisterSingleton(typeof(IRealmBaseService<,>), typeof(RealmBaseService<,>));

            //containerRegistry.Register<IRealmConnection, RealmConnection>();
            //containerRegistry.Register<IBaseRepository, BaseRepository>();
        }

        public static void RegisterPages(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>("MainPage");
            containerRegistry.RegisterForNavigation<NoPhotosPage, NoPhotosPageViewModel>("NoPhotosPage");

            
        }
    }
}