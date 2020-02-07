using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V4.App;
using Bizland.Core.Droid.DependencyService;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(ProgressUpdateService))]
namespace Bizland.Core.Droid.DependencyService
{
    public class ProgressUpdateService : IProgressUpdate
    {
        static NotificationManager mNotifyManager;
        static NotificationCompat.Builder mBuilder;

        public ProgressUpdateService()
        {
            mNotifyManager = (NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService);
            mBuilder = new NotificationCompat.Builder(Android.App.Application.Context, "2")
              .SetContentTitle("Loading")
              .SetProgress(100, 0, false)
              .SetSmallIcon(17301634);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                Intent notificationIntent = new Intent(Android.App.Application.Context, Android.App.Application.Context.Class);
                PendingIntent notificationPendingIntent = PendingIntent.GetActivity(Android.App.Application.Context, 1, notificationIntent, PendingIntentFlags.UpdateCurrent);
                mBuilder.SetContentIntent(notificationPendingIntent);
                NotificationChannel mChannel = new NotificationChannel("2", "haha", Android.App.NotificationImportance.Low);
                mNotifyManager.CreateNotificationChannel(mChannel);
            }
        }


        public void ProgressUpdate(int id, int ProgressPercentage)
        {
            mBuilder.SetProgress(100, ProgressPercentage, false).SetContentText(ProgressPercentage + "%");
            mNotifyManager.Notify(id, mBuilder.Build());
        }
        public void ProgressCancel(int id)
        {
            ((NotificationManager)Android.App.Application.Context.GetSystemService(Context.NotificationService)).Cancel(id);
        }
    }
}