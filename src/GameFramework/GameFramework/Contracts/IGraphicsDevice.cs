// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Contracts
{
    public interface IGraphicsDevice
    {
        ISwapChain CreateSwapChain(float width, float height, float dpi, AlphaMode alphaMode);

        ISwapChain CreateSwapChain(IPlatformWindow window, float dpi);
    }
}
