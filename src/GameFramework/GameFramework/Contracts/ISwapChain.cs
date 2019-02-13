// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Core;
using System.Drawing;

namespace GameFramework.Contracts
{
    public interface ISwapChain
    {
        AlphaMode AlphaMode { get; set; }

        float Dpi { get; set; }

        float Width { get; set; }

        float Height { get; set; }

        Size Size { get; set; }

        ISwapChain Target { get; set; }

        void ResizeBuffersWithWidthAndHeightAndDpi(
            float newWidth,
            float newHeight,
            float newDpi);

        // IDrawingSession CreateDrawingSession(Colors.Black);
        IDrawingSession CreateDrawingSession();

        void Present();
    }
}
