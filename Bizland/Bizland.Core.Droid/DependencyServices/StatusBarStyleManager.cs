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
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarStyleManager))]
namespace Bizland.Core.Droid.DependencyServices
{
    public class StatusBarStyleManager : IStatusBarStyleManager
    {
        public void SetDarkTheme()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            activity.Window.DecorView.SystemUiVisibility = 0;
        }

        public void SetLightTheme()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LightStatusBar;
        }

    }
}