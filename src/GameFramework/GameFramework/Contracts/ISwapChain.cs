// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Core;
using System.Numerics;

namespace GameFramework.Contracts
{
    public interface ISwapChain
    {
        AlphaMode AlphaMode { get; set; }

        float LogicalDpi { get; }

        double Width { get; }

        double Height { get; }

        ISwapChain Target { get; set; }

        void ResizeBuffersWithWidthAndHeightAndDpi(float newWidth, float newHeight, float newDpi);

        IDrawingSession CreateDrawingSession();

        void Present();

        void WaitForVerticalBlank();

        void CreateSwapChain();
    }
}
