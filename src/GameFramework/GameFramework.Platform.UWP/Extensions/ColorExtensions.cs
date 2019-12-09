// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace GameFramework.Platform.Extensions
{
    public static class ColorExtensions
    {
        public static Windows.UI.Color ToColor(this GameFramework.Abstractions.Color color)
        {
            return new Windows.UI.Color
            {
                R = color.R,
                G = color.G,
                B = color.B,
                A = color.A,
            };
        }
    }
}
