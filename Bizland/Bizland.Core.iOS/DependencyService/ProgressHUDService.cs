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
            hud.Hide(true, 3.0f);
            hud = null;
        }

        public void StartHUD(string message)
        {
            UIViewController controller =
        UIApplication.SharedApplication.KeyWindow.RootViewController;
            hud = new MBProgressHUD(controller.View);
            controller.View.AddSubview(hud);
            hud.ContentColor = UIColor.Clear;
            // Add information to your HUD
            hud.Label.Text = message;
            // Set custom view mode
            hud.Mode = MBProgressHUDMode.CustomView;
            hud.Margin = 0f;
            // The sample image is based on the work by http://www.pixelpressicons.com, http://creativecommons.org/licenses/by/2.5/ca/
            // Make the customViews 37 by 37 pixels for best results (those are the bounds of the build-in progress indicators)
            hud.CustomView = new UIImageView(UIImage.FromBundle("logoprosess.png"));

            hud.Show(true);
        }
    }
}