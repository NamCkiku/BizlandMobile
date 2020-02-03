using System;

namespace Bizland.Core
{
    public sealed class PinDragEventArgs : EventArgs
    {
        public Pin Pin
        {
            get;
            private set;
        }

        public PinDragEventArgs(Pin pin)
        {
            this.Pin = pin;
        }
    }
}