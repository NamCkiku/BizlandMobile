using System;

namespace Bizland.Core
{
    public sealed class CameraChangedEventArgs : EventArgs
    {
        public CameraPosition Position
        {
            get;
        }

        public CameraChangedEventArgs(CameraPosition position)
        {
            Position = position;
        }
    }
}