// Copyright (c) Peter Nylander.  All rights reserved.

namespace GameFramework.Extensions
{
    public static class ColorExtensions
    {
        public static Windows.UI.Color ToColor(this GameFramework.Graphics.Color color)
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
