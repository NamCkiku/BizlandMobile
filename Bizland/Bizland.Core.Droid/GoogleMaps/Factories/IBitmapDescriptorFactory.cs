using AndroidBitmapDescriptor = Android.Gms.Maps.Model.BitmapDescriptor;

namespace Bizland.Core.Droid.Factories
{
    public interface IBitmapDescriptorFactory
    {
        AndroidBitmapDescriptor ToNative(BitmapDescriptor descriptor);
    }
}