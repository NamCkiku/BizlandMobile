using System;

namespace Bizland.Core
{
    public sealed class InfoWindowLongClickedEventArgs : EventArgs
    {
        public Pin Pin { get; }

        public InfoWindowLongClickedEventArgs(Pin pin)
        {
            this.Pin = pin;
        }
    }
}