// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace GameFramework.Abstractions
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
