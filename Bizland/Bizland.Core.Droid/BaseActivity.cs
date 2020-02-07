using Bizland.Core.Droid.DependencyServices;
using Prism;
using Prism.Ioc;

namespace Bizland.Core.Droid
{
    public class BaseActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        //{
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}

        public class AndroidInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations
                containerRegistry.RegisterInstance<IDisplayMessage>(new DisplayMessageService());
                containerRegistry.RegisterInstance<ISettingsService>(new SettingsService());
                containerRegistry.RegisterInstance<ITooltipService>(new DroidTooltipService());
                containerRegistry.RegisterInstance<IAudioManager>(new DroidAudioManager());
                containerRegistry.RegisterInstance<IStatusBarStyleManager>(new StatusBarStyleManager());
            }
        }
    }
}