// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;

namespace GameFramework.Abstractions
{
    /// <summary>
    /// Represents a drawing session.
    /// </summary>
    public interface IDrawingSession : IDisposable
    {
        void Clear(Color color);

        /// <summary>
        /// Draw a resource using a transform and an optional tint.
        /// </summary>
        /// <param name="texture">The texture to be drawn.</param>
        /// <param name="transform">The transform to apply.</param>
        /// <param name="tint">An optional tint.</param>
        void Draw(ITexture texture, Matrix3x2 transform, Vector4 tint);

        void DrawText(string text, Vector2 position, Color color);

        void Close();
    }
}
