using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bizland.Core.Droid.DependencyServices;
using Com.JeevanDeshmukh.GlideToastLib;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(DisplayMessageService))]
namespace Bizland.Core.Droid.DependencyServices
{
    public class DisplayMessageService : IDisplayMessage
    {
        public void ShowMessageError(string message, double time)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            new GlideToast.MakeToast(activity, message, (int)time, GlideToast.FailToast, GlideToast.Bottom, Bizland.Core.Droid.Resource.Drawable.ic_notifications, "#ffffff").Show();
        }

        public void ShowMessageInfo(string message, double time)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            new GlideToast.MakeToast(activity, message, (int)time, GlideToast.InfoToast, GlideToast.Bottom, Bizland.Core.Droid.Resource.Drawable.ic_notifications, "#ffffff").Show();
        }

        public void ShowMessageWarning(string message, double time)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            new GlideToast.MakeToast(activity, message, (int)time, GlideToast.WarningToast, GlideToast.Bottom, Bizland.Core.Droid.Resource.Drawable.ic_notifications, "#ffffff").Show();
        }

        public void ShowMessageSuccess(string message, double time)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            new GlideToast.MakeToast(activity, message, (int)time, GlideToast.SuccessToast, GlideToast.Bottom, Bizland.Core.Droid.Resource.Drawable.ic_notifications, "#ffffff").Show();
        }

        public void ShowToast(string message, double time)
        {
            Activity activity = CrossCurrentActivity.Current.Activity;
            new GlideToast.MakeToast(activity, message, (int)time, GlideToast.FailToast, GlideToast.Bottom, Bizland.Core.Droid.Resource.Drawable.ic_notifications, "#ffffff").Show();
        }
    }
}