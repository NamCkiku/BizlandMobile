using Android.Bluetooth;
using Android.Content;
using Android.Locations;

using Bizland.Core.Droid.DependencyServices;

using Plugin.CurrentActivity;

using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsService))]

namespace Bizland.Core.Droid.DependencyServices
{
    public class SettingsService : ISettingsService
    {
        public bool OpenBluetoothSettings()
        {
            BluetoothManager bluetoothManager = (BluetoothManager)Plugin.CurrentActivity.CrossCurrentActivity.Current.AppContext.GetSystemService(Context.BluetoothService);

            if (!bluetoothManager.Adapter.IsEnabled)
            {
                Intent bluSettingIntent = new Intent(Android.Provider.Settings.ActionBluetoothSettings);

                CrossCurrentActivity.Current.AppContext.StartActivity(bluSettingIntent);
            }
            return true;
        }

        public bool OpenLocationSettings()
        {
            LocationManager locationManager = (LocationManager)Plugin.CurrentActivity.CrossCurrentActivity.Current.AppContext.GetSystemService(Context.LocationService);

            if (locationManager.IsProviderEnabled(LocationManager.GpsProvider) == false)
            {
                Intent gpsSettingIntent = new Intent(Android.Provider.Settings.ActionLocat‌​ionSourceSettings);
                CrossCurrentActivity.Current.AppContext.StartActivity(gpsSettingIntent);
            }
            return true;
        }

        public bool OpenWifiSettings()
        {
            CrossCurrentActivity.Current.AppContext.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionWifiSettings));
            return true;
        }
    }
}