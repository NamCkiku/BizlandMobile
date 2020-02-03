using Com.OneSignal;
using Com.OneSignal.Abstractions;

using System.Diagnostics;

namespace Bizland.Core.Helpers
{
    public static class OneSignalHelper
    {
        public static void RegisterOneSignal(string oneSignalKey)
        {
            //Đăng kí sử dụng OneSignal
            OneSignal.Current.StartInit(oneSignalKey)
                 .HandleNotificationReceived(HandleNotificationReceived)
                 .HandleNotificationOpened(HandleNotificationOpened)
                 .InFocusDisplaying(OSInFocusDisplayOption.Notification)
                 .EndInit();

            OneSignal.Current.IdsAvailable((playerID, pushToken) =>
            {
                Debug.WriteLine("OneSignal.Current.IdsAvailable:D playerID: {0}, pushToken: {1}", playerID, pushToken);
            });
        }

        public static void HandleNotificationReceived(OSNotification notification)
        {
            OSNotificationPayload payload = notification.payload;
            if (payload.additionalData.TryGetValue("Types", out var types))
            {
                var t = types.ToString();
                //trả về parameter thích làm gì thì làm
                if (payload.additionalData.TryGetValue("Value", out var values))
                {
                };
            };
        }

        public static void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            if (result.notification.payload.additionalData.TryGetValue("Types", out var types))
            {
                var t = types.ToString();
                //trả về parameter thích làm gì thì làm
                if (result.notification.payload.additionalData.TryGetValue("Value", out var values))
                {
                };
            };
        }
    }
}