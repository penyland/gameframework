// Copyright (c) Peter Nylander.  All rights reserved.

using System.Numerics;

namespace GameFramework.Contracts
{
    public interface IGraphicsDevice
    {
        float LogicalDpi { get; set; }

        Vector2 Size { get; set; }

        ISwapChain SwapChain { get; }

        void Present();

        IDrawingSession CreateDrawingSession();
    }
}
