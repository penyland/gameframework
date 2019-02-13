// Copyright (c) Peter Nylander.  All rights reserved.

using System.Numerics;

namespace GameFramework.Contracts
{
    /// <summary>
    /// Represents a drawing session.
    /// </summary>
    public interface IDrawingSession
    {
        /// <summary>
        /// Draw a resource using a transform and an optional tint.
        /// </summary>
        /// <param name="resource">The resource to be drawn.</param>
        /// <param name="transform">The transform to apply.</param>
        /// <param name="tint">An optional tint.</param>
        void Draw(ITexture resource, Matrix3x2 transform, Vector4 tint);

        void Close();
    }
}
