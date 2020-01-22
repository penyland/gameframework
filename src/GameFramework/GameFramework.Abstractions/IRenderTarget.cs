// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace GameFramework.Abstractions
{
    /// <summary>
    /// A rendertarget is a bitmap that can be drawn onto.
    /// </summary>
    public interface IRenderTarget : ITexture
    {
        IDrawingSession CreateDrawingSession();
    }
}
