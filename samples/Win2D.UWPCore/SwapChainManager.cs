// Copyright (c) Peter Nylander.  All rights reserved.

using Microsoft.Graphics.Canvas;
using System;
using System.Diagnostics;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Core;

namespace Win2D.UWPCore
{
    internal class SwapChainManager
    {
        public SwapChainManager(CanvasDevice device, Rect bounds)
        {
            float currentDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
            this.SwapChain = new CanvasSwapChain(
                device,
                (float)bounds.Width,
                (float)bounds.Height,
                currentDpi);
        }

        public SwapChainManager(CoreWindow window, CanvasDevice device)
        {
            float currentDpi = DisplayInformation.GetForCurrentView().LogicalDpi;
            this.SwapChain = CanvasSwapChain.CreateForCoreWindow(device, window, currentDpi);
        }

        public CanvasSwapChain SwapChain { get; private set; }

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

        public void EnsureMatchesWindow(Rect bounds)
        {
            var windowSize = new Size(bounds.Width, bounds.Height);
            float dpi = DisplayInformation.GetForCurrentView().LogicalDpi;

            if (!SizeEqualsWithTolerance(windowSize, this.SwapChain.Size) || dpi != this.SwapChain.Dpi)
            {
                // Note: swapchain size & window size may not be exactly equal since they are represented with
                // floating point numbers and are calculated via different code paths.
                this.SwapChain.ResizeBuffers((float)windowSize.Width, (float)windowSize.Height, dpi);
            }
        }
    }
}
