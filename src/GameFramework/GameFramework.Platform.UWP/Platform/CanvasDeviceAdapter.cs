// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Core;
using Microsoft.Graphics.Canvas;
using System;

namespace GameFramework.Platform
{
    public class CanvasDeviceAdapter : IGraphicsDevice
    {
        private readonly CanvasDevice canvasDevice;

        public CanvasDeviceAdapter(CanvasDevice canvasDevice)
        {
            this.canvasDevice = canvasDevice ?? throw new ArgumentNullException(nameof(canvasDevice));
        }

        public ISwapChain CreateSwapChain(float width, float height, float dpi, AlphaMode alphaMode)
        {
            // AlphaMode to CanvasAlphaMode
            var swapChain = new CanvasSwapChain(this.canvasDevice, width, height, dpi, Windows.Graphics.DirectX.DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, CanvasAlphaMode.Ignore);

            return new CanvasSwapChainAdapter(swapChain);
        }

        public ISwapChain CreateSwapChain(IPlatformWindow window, float dpi)
        {
            Windows.UI.Core.CoreWindow coreWindow = ((CoreWindowAdapter)window).Window;
            var swapChain = CanvasSwapChain.CreateForCoreWindow(this.canvasDevice, coreWindow, dpi);

            return new CanvasSwapChainAdapter(swapChain);
        }
    }
}
