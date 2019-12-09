// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using Microsoft.Graphics.Canvas;
using System;
using System.Threading.Tasks;

namespace GameFramework.Platform.Graphics
{
    public sealed class CanvasBitmapResourceLoader : ITextureResourceLoader
    {
        private readonly IGraphicsDeviceAdapter graphicsDeviceAdapter;
        private CanvasDevice canvasDevice;

        public CanvasBitmapResourceLoader(IGraphicsDeviceAdapter graphicsDeviceAdapter)
        {
            this.graphicsDeviceAdapter = graphicsDeviceAdapter ?? throw new ArgumentNullException(nameof(graphicsDeviceAdapter));
        }

        public bool CanLoadResource(string uri)
        {
            throw new NotImplementedException();
        }

        public async Task<ITexture> LoadResourceAsync(string uri)
        {
            if (this.canvasDevice == null)
            {
                this.canvasDevice = ((CanvasDeviceAdapter)this.graphicsDeviceAdapter).CanvasDevice;
            }

            CanvasBitmap bitmap = await CanvasBitmap.LoadAsync(this.canvasDevice, new Uri(uri));
            return new Texture(bitmap);
        }
    }
}
