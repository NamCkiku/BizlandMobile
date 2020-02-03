using System;

namespace Bizland.Core
{
    public sealed class MyLocationButtonClickedEventArgs : EventArgs
    {
        public bool Handled { get; set; } = false;

        public MyLocationButtonClickedEventArgs()
        {
        }
    }
}