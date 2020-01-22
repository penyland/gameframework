// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using Microsoft.Graphics.Canvas;
using System;

namespace GameFramework.Platform.Graphics
{
    public class CanvasRenderTargetFactory : IPlatformRenderTargetFactory
    {
        private readonly IGraphicsDeviceAdapter graphicsDeviceAdapter;

        public CanvasRenderTargetFactory(IGraphicsDeviceAdapter graphicsDeviceAdapter)
        {
            this.graphicsDeviceAdapter = graphicsDeviceAdapter ?? throw new ArgumentNullException(nameof(graphicsDeviceAdapter));
        }

        public IRenderTarget CreateRenderTarget(int width, int height)
        {
            CanvasDevice canvasDevice = ((CanvasDeviceAdapter)this.graphicsDeviceAdapter).CanvasDevice;

            var canvasRenderTarget = new CanvasRenderTarget(canvasDevice, width, height, 96);

            return new RenderTarget(canvasRenderTarget);
        }
    }
}
