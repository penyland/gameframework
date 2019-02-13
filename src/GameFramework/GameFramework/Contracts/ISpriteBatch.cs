// Copyright (c) Peter Nylander.  All rights reserved.

using System.Numerics;

namespace GameFramework.Contracts
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
