// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Core;
using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;

namespace GameFramework.Platform
{
    public class CanvasSwapChainManager : ISwapChain
    {
        public CanvasSwapChainManager(CoreWindow window, CanvasDevice device)
        {
            float currentDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
            this.SwapChain = CanvasSwapChain.CreateForCoreWindow(device, window, currentDpi);
            this.Window = window;
        }

        public CanvasSwapChainManager(CanvasSwapChain canvasSwapChain)
        {
            this.SwapChain = canvasSwapChain;
        }

        public CanvasSwapChain SwapChain { get; private set; }

        public CoreWindow Window { get; }

        public AlphaMode AlphaMode { get; set; }

        public float Dpi { get; set; }

        public ISwapChain Target { get; set; }

        public float Width { get; set; }

        public float Height { get; set; }

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
                this.SwapChain.Size) ||
                newDpi != this.SwapChain.Dpi)
            {
                this.SwapChain.ResizeBuffers(newWidth, newHeight, newDpi);
            }
        }

        public IDrawingSession CreateDrawingSession()
        {
            var canvasDrawingSession = this.SwapChain.CreateDrawingSession(Colors.CornflowerBlue);

            return new DrawingSession(canvasDrawingSession);
        }

        public void Present()
        {
            try
            {
                this.SwapChain.Present();
            }
            catch (Exception e) when (this.SwapChain.Device.IsDeviceLost(e.HResult))
            {
                this.SwapChain.Device.RaiseDeviceLost();
            }
        }

        public void WaitForVerticalBlank()
        {
            this.SwapChain?.WaitForVerticalBlank();
        }
    }
}
