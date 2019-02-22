// Copyright (c) Peter Nylander.  All rights reserved.

using System.Numerics;
using Windows.Foundation;

namespace GameFramework.Extensions
{
    public static class RectExtensions
    {
        public static Vector2 ToVector2(this Rect rect)
        {
            return new Vector2((float)rect.Width, (float)rect.Height);
        }
    }
}
