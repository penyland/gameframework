// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;

namespace GameFramework.Utils
{
    public static class MathUtils
    {
        /// <summary>
        /// Represents the value of pi divided by two(1.57079637).
        /// </summary>
        public const float PiOver2 = (float)(Math.PI / 2.0);

        /// <summary>
        /// Represents the value of pi divided by four(0.7853982).
        /// </summary>
        public const float PiOver4 = (float)(Math.PI / 4.0);

        /// <summary>
        /// Represents the value of pi times two(6.28318548).
        /// </summary>
        public const float TwoPi = (float)(Math.PI * 2.0);

        public static float ToRadians(float angle)
        {
            return (float)(Math.PI / 180) * angle;
        }

        public static double ToDegrees(float radians)
        {
            return radians * (180 / Math.PI);
        }

        public static float Distance(float value1, float value2)
        {
            return Math.Abs(value1 - value2);
        }

        public static float Lerp(float value1, float value2, float amount)
        {
            return value1 + ((value2 - value1) * amount);
        }

        public static Matrix3x2 CreateTransformMatrix(float rotation, float scale, Vector2 position, Vector2 centerPoint)
        {
            return Matrix3x2.CreateRotation(rotation, centerPoint) *
                    Matrix3x2.CreateScale(scale, centerPoint) *
                    Matrix3x2.CreateTranslation(position - centerPoint);
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(float value, float min, float max)
        {
            // First we check to see if we're greater than the max
            value = (value > max) ? max : value;

            // Then we check to see if we're less than the min.
            // There's no check to see if min > max.
            return (value < min) ? min : value;
        }

        /// <summary>
        /// Restricts a value to be within a specified range.
        /// </summary>
        /// <param name="value">The value to clamp.</param>
        /// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
        /// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
        /// <returns>The clamped value.</returns>
        public static int Clamp(int value, int min, int max)
        {
            value = (value > max) ? max : value;
            return (value < min) ? min : value;
        }
    }
}
