using Bizland.Core.iOS.DependencyService;
using Prism;
using Prism.Ioc;

using UIKit;

using UserNotifications;

namespace Bizland.Core.iOS
{
    public class BaseDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        protected void RequestNotificationPermissions(UIApplication app)
        {
            // Request Permissions
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Request Permissions
                UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (granted, error) =>
                {
                    // Do something if needed
                });
            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                app.RegisterUserNotificationSettings(UIUserNotificationSettings.GetSettingsForTypes(UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound, null));
            }
        }

        public override void PerformActionForShortcutItem(UIApplication application, UIApplicationShortcutItem shortcutItem, UIOperationHandler completionHandler)
        {
            var uri = Plugin.AppShortcuts.iOS.ArgumentsHelper.GetUriFromApplicationShortcutItem(shortcutItem);
            if (uri != null)
            {
                Xamarin.Forms.Application.Current.SendOnAppLinkRequestReceived(uri);
            }
        }

        public class IOSInitializer : IPlatformInitializer
        {
            public void RegisterTypes(IContainerRegistry containerRegistry)
            {
                // Register any platform specific implementations
                containerRegistry.RegisterInstance<IDisplayMessage>(new DisplayMessageService());
                containerRegistry.RegisterInstance<ISettingsService>(new SettingsService());
                containerRegistry.RegisterInstance<ITooltipService>(new iOSTooltipService());
                containerRegistry.RegisterInstance<IAudioManager>(new AppleAudioManager());
                containerRegistry.RegisterInstance<IStatusBarStyleManager>(new StatusBarStyleManager());
            }
        }
    }
}