using Bizland.Core.ViewModels;
using Bizland.Core.Views;
using Bizland.Entities.Infrastructure.Repository;
using Bizland.Service;
using Bizland.Utilities.Constant;
using Prism.Ioc;
using Prism.Navigation;

using Xamarin.Forms;

namespace Bizland.Core.Setup
{
    public static class BizlandSetup
    {
        public static void Initialize()
        {
            //Đăng kí sử dụng Syncfusion
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Config.SyncfusionKey);

            CultureHelper.SetCulture();
        }

        public static void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton(typeof(IRealmBaseService<,>), typeof(RealmBaseService<,>));

            containerRegistry.Register<IRealmConnection, RealmConnection>();
            containerRegistry.Register<IBaseRepository, BaseRepository>();

            containerRegistry.RegisterSingleton<IRequestProvider, RequestProvider>();
            containerRegistry.Register<IPlacesAutocomplete, PlacesAutocomplete>();
            containerRegistry.Register<IPlacesGeocode, PlacesGeocode>();
            //containerRegistry.Register<ISignalRServices, SignalRService>();
        }

        public static void RegisterPages(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RootPage, RootPageViewModel>("RootPage");
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<BaseNavigationPage, BaseNavigationPageViewModel>("BaseNavigationPage");
            containerRegistry.RegisterDialog<DialogView, DialogViewViewModel>("DialogView");
            containerRegistry.RegisterForNavigation<NoPhotosPage, NoPhotosPageViewModel>("NoPhotosPage");
            containerRegistry.RegisterForNavigation<OnBoardingPage, OnBoardingPageViewModel>("OnBoardingPage");
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>("LoginPage");
            containerRegistry.RegisterForNavigation<RegisterPage, RegisterPageViewModel>("RegisterPage");
            containerRegistry.RegisterForNavigation<OTPPage, OTPPageViewModel>("OTPPage");
            containerRegistry.RegisterForNavigation<SomethingWentWrongPage, SomethingWentWrongPageViewModel>("SomethingWentWrongPage");
            containerRegistry.RegisterForNavigation<NoPhotosPage, NoPhotosPageViewModel>("NoPhotosPage");
            containerRegistry.RegisterForNavigation<NoItemPage, NoItemPageViewModel>("NoItemPage");
            containerRegistry.RegisterForNavigation<NoInternetConnectionPage, NoInternetConnectionPageViewModel>("NoInternetConnectionPage");
            containerRegistry.RegisterForNavigation<LocationDeniedPage, LocationDeniedPageViewModel>("LocationDeniedPage");
            containerRegistry.RegisterForNavigation<CountryPage, CountryPageViewModel>("CountryPage");
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>("HomePage");


        }
    }
}