using System;

namespace Bizland.Core
{
    public sealed class SelectedPinChangedEventArgs : EventArgs
    {
        public Pin SelectedPin
        {
            get;
            private set;
        }

        public SelectedPinChangedEventArgs(Pin selectedPin)
        {
            this.SelectedPin = selectedPin;
        }
    }
}