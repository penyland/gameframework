// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Core;
using Microsoft.Graphics.Canvas;
using System;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI;
using Windows.UI.Core;

namespace GameFramework.Platform
{
    public class CanvasSwapChainAdapter : ISwapChain
    {
        public CanvasSwapChainAdapter(CoreWindow window, CanvasDevice device)
        {
            float currentDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
            this.SwapChain = CanvasSwapChain.CreateForCoreWindow(device, window, currentDpi);
            this.Window = window;
        }

        public CanvasSwapChainAdapter(CanvasSwapChain canvasSwapChain)
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

        public System.Drawing.Size Size { get; set; }

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

        public void EnsureMatchesWindow(CoreWindow window)
        {
            Rect bounds = window.Bounds;
            var windowSize = new Size(bounds.Width, bounds.Height);
            float dpi = DisplayInformation.GetForCurrentView().LogicalDpi;

            if (!SizeEqualsWithTolerance(windowSize, this.SwapChain.Size) || dpi != this.SwapChain.Dpi)
            {
                // Note: swapchain size & window size may not be exactly equal since they are represented with
                // floating point numbers and are calculated via different code paths.
                this.SwapChain.ResizeBuffers((float)windowSize.Width, (float)windowSize.Height, dpi);
            }
        }

        public void ResizeBuffersWithWidthAndHeightAndDpi(float newWidth, float newHeight, float newDpi)
        {
            this.SwapChain.ResizeBuffers(newWidth, newHeight, newDpi);
        }

        public IDrawingSession CreateDrawingSession()
        {
            var ds = new DrawingSession(this.SwapChain.CreateDrawingSession(Colors.CornflowerBlue));

            return ds;
        }

        public void Present()
        {
            this.SwapChain.Present();
        }
    }
}
