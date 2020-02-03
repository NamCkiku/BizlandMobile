using System;

namespace Bizland.Core
{
    public sealed class InfoWindowClickedEventArgs : EventArgs
    {
        public Pin Pin { get; }

        public InfoWindowClickedEventArgs(Pin pin)
        {
            this.Pin = pin;
        }
    }
}