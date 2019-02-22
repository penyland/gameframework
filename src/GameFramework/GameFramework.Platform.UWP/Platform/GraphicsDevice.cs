// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;
using Windows.Foundation;
using Windows.Graphics.Display;

namespace GameFramework.Platform
{
    public class GraphicsDevice : IGraphicsDevice
    {
        private readonly IPlatformWindow window;

        private CanvasDevice canvasDevice;
        private Vector2 size;
        private float dpi;

        public GraphicsDevice(IPlatformWindow window)
        {
            this.window = window ?? throw new ArgumentNullException(nameof(window));
            this.size = this.window.Size;
            this.dpi = DisplayInformation.GetForCurrentView().LogicalDpi;

            this.CreateDeviceResources();
        }

        public float LogicalDpi
        {
            get => this.dpi;
            set
            {
                if (value != this.dpi)
                {
                    this.dpi = value;

                    this.CreateWindowSizeDependentResources();
                }
            }
        }

        public Vector2 Size
        {
            get => this.size;
            set
            {
                if (value != this.size)
                {
                    this.size = value;
                    this.CreateWindowSizeDependentResources();
                }
            }
        }

        public ISwapChain SwapChain { get; internal set; }

        public void Present()
        {
            this.SwapChain?.Present();
        }

        public IDrawingSession CreateDrawingSession()
        {
            return this.SwapChain.CreateDrawingSession();
        }

        private void CreateDeviceResources()
        {
            this.canvasDevice = new CanvasDevice();
            this.canvasDevice.DeviceLost += this.OnDeviceLost;
        }

        private void OnDeviceLost(CanvasDevice sender, object args)
        {
            // Recreate device resources
            this.CreateDeviceResources();
            this.CreateWindowSizeDependentResources();

            // TODO Raise DeviceRestored
        }

        private void CreateDeviceIndependentResources()
        {
        }

        /// <summary>
        /// Create the resources that need to be recreated every time the window size is changed.
        /// </summary>
        private void CreateWindowSizeDependentResources()
        {
            if (this.SwapChain != null)
            {
                // If the swap chain exists, resize it
                Rect windowBounds = ((CoreWindowAdapter)this.window).Window.Bounds;
                this.SwapChain.ResizeBuffersWithWidthAndHeightAndDpi(
                    (float)windowBounds.Width,
                    (float)windowBounds.Height,
                    this.LogicalDpi);
            }
            else
            {
                this.SwapChain = new CanvasSwapChainManager(((CoreWindowAdapter)this.window).Window, this.canvasDevice);
            }
        }
    }
}
