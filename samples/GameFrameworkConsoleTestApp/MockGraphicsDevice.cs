// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using System;
using System.Numerics;

namespace GameFrameworkConsoleTestApp
{
    internal class MockGraphicsDevice : IGraphicsDevice
    {
        public float LogicalDpi { get; set; }

        public Vector2 Size { get; set; }

        public ISwapChain SwapChain { get; }

        public void Clear(Vector4 color)
        {
            throw new NotImplementedException();
        }

        public IDrawingSession CreateDrawingSession()
        {
            throw new NotImplementedException();
        }

        public void Present()
        {
            throw new NotImplementedException();
        }

        public void SetWindow(IPlatformWindow platformWindow)
        {
            throw new NotImplementedException();
        }
    }
}
