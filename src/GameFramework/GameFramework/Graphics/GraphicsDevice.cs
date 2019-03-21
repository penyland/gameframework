// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using System;
using System.Numerics;

namespace GameFramework.Platform
{
    public sealed class GraphicsDevice : IGraphicsDevice
    {
        private readonly IPlatformWindow window;
        private readonly IGraphicsDeviceAdapter graphicsDeviceAdapter;
        private readonly ISwapChain swapChain;
        private Vector2 size;
        private float dpi;

        public GraphicsDevice(
            IPlatformWindow window,
            IGraphicsDeviceAdapter graphicsDeviceAdapter,
            ISwapChain swapChainAdapter)
        {
            this.window = window ?? throw new ArgumentNullException(nameof(window));
            this.graphicsDeviceAdapter = graphicsDeviceAdapter ?? throw new ArgumentNullException(nameof(graphicsDeviceAdapter));
            this.swapChain = swapChainAdapter ?? throw new ArgumentNullException(nameof(swapChainAdapter));

            this.size = this.window.Size;
            this.dpi = this.swapChain.LogicalDpi;

            this.CreateDeviceResources();
            this.CreateDeviceIndependentResources();
            this.CreateWindowSizeDependentResources();
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

        public ISwapChain SwapChain => this.swapChain;

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
            this.graphicsDeviceAdapter.CreateDeviceResources();
            this.graphicsDeviceAdapter.DeviceLost += this.OnDeviceLost;
        }

        private void OnDeviceLost(object sender, object args)
        {
            this.graphicsDeviceAdapter.DeviceLost -= this.OnDeviceLost;

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
            if (this.SwapChain.Target != null)
            {
                // If the swap chain exists, resize it
                Vector2 size = this.window.Size;

                this.SwapChain.ResizeBuffersWithWidthAndHeightAndDpi(
                    (float)size.X,
                    (float)size.Y,
                    this.LogicalDpi);
            }
            else
            {
                this.swapChain.CreateSwapChain();
            }
        }
    }
}
