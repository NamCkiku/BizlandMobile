using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace Bizland.Core.iOS.Extensions
{
    public static class ImageSourceExtensions
    {
        static ImageLoaderSourceHandler s_imageLoaderSourceHandler;

        static ImageSourceExtensions()
        {
            s_imageLoaderSourceHandler = new ImageLoaderSourceHandler();
        }
        public static Task<UIImage> ToUIImage(this ImageSource imageSource)
        {
            return s_imageLoaderSourceHandler.LoadImageAsync(imageSource);
        }
    }
}