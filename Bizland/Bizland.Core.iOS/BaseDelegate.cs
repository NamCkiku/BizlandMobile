using Bizland.Core.iOS.DependencyService;
using Foundation;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
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

        public override void ReceivedLocalNotification(UIApplication application, UILocalNotification notification)
        {

            // Local Notifications are received here
        }


        public override void OnActivated(UIApplication uiApplication)
        {
            base.OnActivated(uiApplication);
        }


        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            GoogleClientManager.OnOpenUrl(app, url, options);
            return FacebookClientManager.OpenUrl(app, url, options);
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            return FacebookClientManager.OpenUrl(application, url, sourceApplication, annotation);
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