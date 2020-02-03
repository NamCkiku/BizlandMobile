// Original code from https://github.com/javiholcman/Wapps.Forms.Map/
// Cacheing implemented by Gadzair

using Android.Graphics;
using Android.Views;

using System.Threading.Tasks;

namespace Bizland.Core.Droid
{
    public class UtilsCluster
    {
        /// <summary>
        /// convert from dp to pixels
        /// </summary>
        /// <param name="dp">Dp.</param>
        public static int DpToPx(float dp)
        {
            var metrics = global::Android.App.Application.Context.Resources.DisplayMetrics;
            return (int)(dp * metrics.Density);
        }

        public static Bitmap ConvertViewToBitmap(global::Android.Views.View v)
        {
            v.SetLayerType(LayerType.Hardware, null);
            v.DrawingCacheEnabled = true;

            v.Measure(global::Android.Views.View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified),
                global::Android.Views.View.MeasureSpec.MakeMeasureSpec(0, MeasureSpecMode.Unspecified));
            v.Layout(0, 0, v.MeasuredWidth, v.MeasuredHeight);

            v.BuildDrawingCache(true);
            Bitmap b = Bitmap.CreateBitmap(v.GetDrawingCache(true));
            v.DrawingCacheEnabled = false; // clear drawing cache
            return b;
        }

        public static Task<global::Android.Gms.Maps.Model.BitmapDescriptor> ConvertViewToBitmapDescriptor(global::Android.Views.View v)
        {
            return Task.Run(() =>
            {
                var bmp = ConvertViewToBitmap(v);
                var img = global::Android.Gms.Maps.Model.BitmapDescriptorFactory.FromBitmap(bmp);
                return img;
            });
        }
    }
}