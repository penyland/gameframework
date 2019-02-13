// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Numerics;

namespace GameFramework.Extensions
{
    public static class RandomExtensions
    {
        public static float NextFloat(this Random rand, float minValue, float maxValue)
        {
            return ((float)rand.NextDouble() * (maxValue - minValue)) + minValue;
        }

        public static Vector2 NextVector2(this Random rand, float minLength, float maxLength)
        {
            double theta = rand.NextDouble() * 2 * Math.PI;
            float length = rand.NextFloat(minLength, maxLength);
            return new Vector2(length * (float)Math.Cos(theta), length * (float)Math.Sin(theta));
        }
    }
}
