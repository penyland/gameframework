// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Graphics.Canvas;
using System;

namespace GameFramework.Platform
{
    public class CanvasDeviceAdapter : IGraphicsDeviceAdapter
    {
        private CanvasDevice canvasDevice;

        public CanvasDeviceAdapter()
        {
        }

        public event EventHandler DeviceLost;

        public CanvasDevice CanvasDevice => this.canvasDevice;

        public void CreateDeviceResources()
        {
            this.canvasDevice = new CanvasDevice();
            this.canvasDevice.DeviceLost -= this.OnDeviceLost;
            this.canvasDevice.DeviceLost += this.OnDeviceLost;
        }

        public void Dispose()
        {
            this.canvasDevice?.Dispose();
        }

        public void Trim()
        {
            this.canvasDevice?.Trim();
        }

        private void OnDeviceLost(CanvasDevice sender, object args)
        {
            this.DeviceLost?.Invoke(this, null);
        }
    }
}
