// Original code from https://github.com/javiholcman/Wapps.Forms.Map/
// Cacheing implemented by Gadzair

using CoreGraphics;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Bizland.Core.iOS
{
    public static class UtilsCluster
    {
        public static UIView ConvertFormsToNative(View view, CGRect size)
        {
            var renderer = Platform.CreateRenderer(view);

            renderer.NativeView.Frame = size;

            renderer.NativeView.AutoresizingMask = UIViewAutoresizing.All;
            renderer.NativeView.ContentMode = UIViewContentMode.ScaleToFill;

            renderer.Element.Layout(size.ToRectangle());

            var nativeView = renderer.NativeView;

            nativeView.SetNeedsLayout();

            return nativeView;
        }

        public static UIImage ConvertViewToImage(Pin vehicle)
        {
            UIImage img = new UIImage();
            var iconView = vehicle.Icon.View;
            var nativeView = UtilsCluster.ConvertFormsToNative(iconView, new CGRect(0, 0, iconView.WidthRequest, iconView.HeightRequest));
            nativeView.BackgroundColor = iconView.BackgroundColor.ToUIColor();
            img = nativeView.AsImage();
            return img;
        }

        public static UIImage AsImage(this UIView view)

        {
            UIGraphics.BeginImageContextWithOptions(view.Bounds.Size, true, 0);

            view.Layer.RenderInContext(UIGraphics.GetCurrentContext());

            var img = UIGraphics.GetImageFromCurrentImageContext();

            UIGraphics.EndImageContext();

            return img;
        }
    }
}