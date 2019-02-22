// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Core;
using System.Numerics;

namespace GameFramework.Contracts
{
    public interface ISwapChain
    {
        AlphaMode AlphaMode { get; set; }

        float Dpi { get; set; }

        float Width { get; set; }

        float Height { get; set; }

        ISwapChain Target { get; set; }

        void ResizeBuffersWithWidthAndHeightAndDpi(float newWidth, float newHeight, float newDpi);

        IDrawingSession CreateDrawingSession();

        void Present();

        void WaitForVerticalBlank();
    }
}
