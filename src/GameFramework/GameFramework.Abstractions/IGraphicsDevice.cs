// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Numerics;

namespace GameFramework.Abstractions
{
    public interface IGraphicsDevice
    {
        float LogicalDpi { get; set; }

        Vector2 Size { get; set; }

        ISwapChain SwapChain { get; }

        void Initialize();

        void Present();

        IDrawingSession CreateDrawingSession();
    }
}
