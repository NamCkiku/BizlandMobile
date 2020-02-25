using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

using Bizland.Core.Droid;
using Bizland.Core.Droid.Setup;

using Xamarin.Forms;

namespace Bizland.Droid
{
    [Activity(Label = "Bizland", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false,
        LaunchMode = LaunchMode.SingleTask, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    [IntentFilter(new[] { Intent.ActionView },
             Categories = new[] { Intent.CategoryDefault },
             DataScheme = "asfs",
             DataHost = "bizland",
             AutoVerify = true)]
    public class MainActivity : BaseActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.SetFlags("FastRenderers_Experimental");
            Forms.SetFlags("CollectionView_Experimental");
            Forms.Init(this, bundle);

            ToolSetup.Initialize(this, bundle);

            LoadApplication(new BizlandApp(new AndroidInitializer()));
        }
    }
}