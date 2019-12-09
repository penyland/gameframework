// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using System;

namespace GameFramework.Utils
{
    public static class ColorHelper
    {
        public static Color HSVToColor(float h, float s, float v)
        {
            if (h == 0 && s == 0)
            {
                return new Color(v, v, v);
            }

            float c = s * v;
            float x = c * (1 - Math.Abs((h % 2) - 1));
            float m = v - c;

            if (h < 1)
            {
                return new Color(c + m, x + m, m);
            }
            else if (h < 2)
            {
                return new Color(x + m, c + m, m);
            }
            else if (h < 3)
            {
                return new Color(m, c + m, x + m);
            }
            else if (h < 4)
            {
                return new Color(m, x + m, c + m);
            }
            else if (h < 5)
            {
                return new Color(x + m, m, c + m);
            }
            else
            {
                return new Color(c + m, m, x + m);
            }
        }
    }
}
