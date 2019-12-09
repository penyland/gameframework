// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace GameFramework.Abstractions
{
    /// <summary>
    /// A SpriteBatch abstraction.
    /// </summary>
    public interface ISpriteBatch
    {
        /// <summary>
        /// Draws a texture.
        /// </summary>
        /// <param name="texture">The resource to draw.</param>
        /// <param name="tint">The color to tint the texture.</param>
        void Draw(ITexture texture, Vector2 position, Vector4 tint, float rotation, Vector2 origin, float scale);
    }
}
