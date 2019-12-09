// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;
using Windows.Foundation;

namespace GameFramework.Platform.Extensions
{
    public static class RectExtensions
    {
        public static Vector2 ToVector2(this Rect rect)
        {
            return new Vector2((float)rect.Width, (float)rect.Height);
        }
    }
}
