// Copyright (c) Peter Nylander.  All rights reserved.

using System.Numerics;

namespace GameFramework
{
    public static class MathUtils
    {
        public static Matrix3x2 CreateTransformMatrix(float rotation, float scale, Vector2 position, Vector2 centerPoint)
        {
            return Matrix3x2.CreateRotation(rotation, centerPoint) *
                    Matrix3x2.CreateScale(scale, centerPoint) *
                    Matrix3x2.CreateTranslation(position - centerPoint);
        }
    }
}
