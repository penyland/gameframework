// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using Microsoft.Graphics.Canvas;
using System;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;

namespace GameFramework.Platform.Graphics
{
    public class CanvasSwapChainAdapter : ISwapChain
    {
        private readonly IPlatformWindow platformWindow;
        private readonly IGraphicsDeviceAdapter graphicsDeviceAdapter;

        private CanvasSwapChain canvasSwapChain;

        public CanvasSwapChainAdapter(
            IPlatformWindow platformWindow,
            IGraphicsDeviceAdapter graphicsDeviceAdapter)
        {
            this.platformWindow = platformWindow ?? throw new ArgumentNullException(nameof(platformWindow));
            this.graphicsDeviceAdapter = graphicsDeviceAdapter ?? throw new ArgumentNullException(nameof(graphicsDeviceAdapter));
        }

        public AlphaMode AlphaMode { get; set; }

        public float LogicalDpi => DisplayInformation.GetForCurrentView().LogicalDpi;

        public ISwapChain Target { get; set; }

        public double Width => this.canvasSwapChain.Size.Width;

        public double Height => this.canvasSwapChain.Size.Height;

        public static bool SizeEqualsWithTolerance(Size sizeA, Size sizeB)
        {
            const float tolerance = 0.1f;

            if (Math.Abs(sizeA.Width - sizeB.Width) > tolerance)
            {
                return false;
            }

            if (Math.Abs(sizeA.Height - sizeB.Height) > tolerance)
            {
                return false;
            }

            return true;
        }

        public void ResizeBuffersWithWidthAndHeightAndDpi(float newWidth, float newHeight, float newDpi)
        {
            if (!SizeEqualsWithTolerance(
                new Windows.Foundation.Size(newWidth, newHeight),
                this.canvasSwapChain.Size) ||
                newDpi != this.canvasSwapChain.Dpi)
            {
                this.canvasSwapChain.ResizeBuffers(newWidth, newHeight, newDpi);
            }
        }

        public IDrawingSession CreateDrawingSession()
        {
            var canvasDrawingSession = this.canvasSwapChain.CreateDrawingSession(Windows.UI.Colors.CornflowerBlue);

            return new DrawingSessionAdapter(canvasDrawingSession);
        }

        public void Present()
        {
            try
            {
                this.canvasSwapChain.Present();
            }
            catch (Exception e) when (this.canvasSwapChain.Device.IsDeviceLost(e.HResult))
            {
                this.canvasSwapChain.Device.RaiseDeviceLost();
            }
        }

        public void WaitForVerticalBlank()
        {
            this.canvasSwapChain?.WaitForVerticalBlank();
        }

        public void CreateSwapChain()
        {
            CanvasDevice canvasDevice = ((CanvasDeviceAdapter)this.graphicsDeviceAdapter).CanvasDevice;
            var coreWindow = (CoreWindow)this.platformWindow.Window;

            float currentDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
            this.canvasSwapChain = CanvasSwapChain.CreateForCoreWindow(canvasDevice, coreWindow, currentDpi);

            this.Target = this;
        }
    }
}
