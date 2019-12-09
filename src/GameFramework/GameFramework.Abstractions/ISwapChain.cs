// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace GameFramework.Abstractions
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
