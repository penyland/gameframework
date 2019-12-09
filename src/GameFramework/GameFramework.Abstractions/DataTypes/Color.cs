// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;

namespace GameFramework.Abstractions
{
    public class Color
    {
        public Color(uint rgba)
        {
            this.A = (byte)((rgba >> 24) & 0xFF);
            this.B = (byte)((rgba >> 16) & 0xFF);
            this.G = (byte)((rgba >> 8) & 0xFF);
            this.R = (byte)(rgba & 0xFF);
        }

        public Color(float r, float g, float b)
            : this((int)r * 255, (int)g * 255, (int)b * 255, 255)
        {
        }

        public Color(float r, float g, float b, float a)
            : this((int)r * 255, (int)g * 255, (int)b * 255, (int)a)
        {
        }

        public Color(int r, int g, int b, int a)
        {
        }

        public Color(byte red, byte green, byte blue, byte alpha)
        {
            this.R = red;
            this.G = green;
            this.B = blue;
            this.A = alpha;
        }

        public byte R { get; }

        public byte G { get; }

        public byte B { get; }

        public byte A { get; }

        public static Color FromArgb(byte alpha, byte red, byte green, byte blue)
        {
            return new Color(red, green, blue, alpha);
        }

        public Vector4 ToVector4()
        {
            return new Vector4(this.R / 255f, this.G / 255f, this.B / 255f, this.A / 255f);
        }
    }
}
