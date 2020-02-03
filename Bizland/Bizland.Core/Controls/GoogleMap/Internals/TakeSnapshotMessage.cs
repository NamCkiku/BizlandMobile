using System;
using System.IO;

namespace Bizland.Core.Internals
{
    public sealed class TakeSnapshotMessage
    {
        public Action<Stream> OnSnapshot { get; }

        public TakeSnapshotMessage(Action<Stream> onSnapshot)
        {
            OnSnapshot = onSnapshot;
        }
    }
}