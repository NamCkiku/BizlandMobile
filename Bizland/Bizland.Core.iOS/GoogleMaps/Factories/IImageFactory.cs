using UIKit;

namespace Bizland.Core.iOS.Factories
{
    public interface IImageFactory
    {
        UIImage ToUIImage(BitmapDescriptor descriptor);
    }
}