using Android.Gms.Maps.Model;

using Bizland.Core.Droid.Extensions;

namespace Bizland.Core.Droid
{
    public static class VisibleRegionExtensions
    {
        public static MapRegion ToRegion(this VisibleRegion visibleRegion)
        {
            return new MapRegion(
                visibleRegion.NearLeft.ToPosition(),
                visibleRegion.NearRight.ToPosition(),
                visibleRegion.FarLeft.ToPosition(),
                visibleRegion.FarRight.ToPosition()
            );
        }
    }
}