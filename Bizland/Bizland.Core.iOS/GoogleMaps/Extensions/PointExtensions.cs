﻿using CoreGraphics;

using System.Drawing;

namespace Bizland.Core.iOS.Extensions
{
    public static class PointExtensions
    {
        public static CGPoint ToCGPoint(this Point self)
        {
            return new CGPoint((float)self.X, (float)self.Y);
        }
    }
}