using System;

namespace Bizland.Core
{
    public sealed class CameraMoveStartedEventArgs : EventArgs
    {
        public bool IsGesture { get; }

        public CameraMoveStartedEventArgs(bool isGesture)
        {
            IsGesture = isGesture;
        }
    }
}