// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using Microsoft.Graphics.Canvas;
using System;

namespace GameFramework.Platform.Graphics
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
