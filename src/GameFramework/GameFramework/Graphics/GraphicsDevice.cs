// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using System;
using System.Numerics;

namespace GameFramework.Graphics
{
    public sealed class GraphicsDevice : IGraphicsDevice
    {
        private readonly IPlatformWindow window;
        private readonly IGraphicsDeviceAdapter graphicsDeviceAdapter;
        private Vector2 size;
        private float dpi;

        public GraphicsDevice(
            IPlatformWindow window,
            IGraphicsDeviceAdapter graphicsDeviceAdapter,
            ISwapChain swapChainAdapter)
        {
            this.window = window ?? throw new ArgumentNullException(nameof(window));
            this.graphicsDeviceAdapter = graphicsDeviceAdapter ?? throw new ArgumentNullException(nameof(graphicsDeviceAdapter));
            this.SwapChain = swapChainAdapter ?? throw new ArgumentNullException(nameof(swapChainAdapter));
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

        public ISwapChain SwapChain { get; }

        public void Initialize()
        {
            this.size = this.window.Size;
            this.dpi = this.SwapChain.LogicalDpi;

            this.CreateDeviceResources();
            this.CreateDeviceIndependentResources();
            this.CreateWindowSizeDependentResources();
        }

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
                this.SwapChain.CreateSwapChain();
            }
        }
    }
}
