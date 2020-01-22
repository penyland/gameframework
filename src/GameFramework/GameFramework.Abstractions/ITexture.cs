// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace GameFramework.Abstractions
{
    /// <summary>
    /// A bitmap is a 2D grid of pixels that form an image.
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

        /// <summary>
        /// Returns an array of raw byte data for the entire bitmap.
        /// </summary>
        byte[] GetData();

        void SetData(byte[] data);
    }
}
