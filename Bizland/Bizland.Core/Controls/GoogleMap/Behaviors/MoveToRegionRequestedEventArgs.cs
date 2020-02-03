using System;

namespace Bizland.Core.GoogleMap.Behaviors
{
    internal sealed class MoveToRegionRequestedEventArgs : EventArgs
    {
        internal MapSpan MapSpan { get; }

        internal bool Animated { get; }

        internal MoveToRegionRequestedEventArgs(MapSpan mapSpan, bool animated)
        {
            MapSpan = mapSpan;
            Animated = animated;
        }
    }
}