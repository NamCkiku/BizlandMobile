using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core.iOS.DependencyService;
using CoreAnimation;
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

            // The sample image is based on the work by http://www.pixelpressicons.com, http://creativecommons.org/licenses/by/2.5/ca/
            // Make the customViews 37 by 37 pixels for best results (those are the bounds of the build-in progress indicators)
            hud.CustomView = new UIImageView(UIImage.FromBundle("logoprosess.png"));


            CABasicAnimation rotation;
            rotation = new CABasicAnimation();
            rotation.KeyPath = "transform.rotation";
            rotation.Duration = 0.7f;
            rotation.RemovedOnCompletion = false;
            hud.CustomView.Layer.AddAnimation(rotation, "Spin");
            // Add information to your HUD
            hud.Label.Text = message;
            hud.BezelView.Color = UIColor.Clear;
            hud.BezelView.TintColor = UIColor.Clear;
            hud.BezelView.Style =MBProgressHUDBackgroundStyle.SolidColor;
            hud.BezelView.BlurEffectStyle = UIBlurEffectStyle.Dark;
            // Set custom view mode
            hud.Mode = MBProgressHUDMode.CustomView;
            rotation.RepeatCount = 500f;
          

            hud.Show(true);
        }
    }
}