using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core.iOS.DependencyService;
using Foundation;
using Ricardo.RMBProgressHUD.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgressHUDService))]
namespace Bizland.Core.iOS.DependencyService
{
    public class ProgressHUDService : IProgressHUDService
    {
        MBProgressHUD hud;
        public ProgressHUDService()
        {
        }

        public void DisposeHUD()
        {
            hud.RemoveFromSuperview();
            hud = null;
        }

        public void StartHUD(string message)
        {
            UIViewController controller =
        UIApplication.SharedApplication.KeyWindow.RootViewController;
            hud = new MBProgressHUD(controller.View);
            controller.View.AddSubview(hud);
            // Add information to your HUD
            hud.Label.Text = message;
            // Set custom view mode
            hud.Mode = MBProgressHUDMode.CustomView;
            // The sample image is based on the work by http://www.pixelpressicons.com, http://creativecommons.org/licenses/by/2.5/ca/
            // Make the customViews 37 by 37 pixels for best results (those are the bounds of the build-in progress indicators)
            hud.CustomView = new UIImageView(UIImage.FromBundle("flag_vn.png"));
            hud.Progress = 10000;
        }
    }
}