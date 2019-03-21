// Copyright (c) Peter Nylander.  All rights reserved.

using System.Numerics;

namespace GameFramework.Contracts
{
    /// <summary>
    /// Represents a particle resource.
    /// </summary>
    public interface ITexture : IResource
    {
        /// <summary>
        /// Gets the center coordinates of the resource.
        /// </summary>
        Vector2 Center { get; }

        /// <summary>
        /// Gets the size of the resource.
        /// </summary>
        Vector2 Size { get; }
    }
}
