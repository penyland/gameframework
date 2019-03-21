// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Graphics.Canvas;
using System;
using System.Threading.Tasks;

namespace GameFramework.Platform
{
    public sealed class CanvasBitmapResourceLoader : ITextureResourceLoader
    {
        private readonly IGraphicsDeviceAdapter graphicsDeviceAdapter;
        private readonly CanvasDevice canvasDevice;

        public CanvasBitmapResourceLoader(IGraphicsDeviceAdapter graphicsDeviceAdapter)
        {
            this.graphicsDeviceAdapter = graphicsDeviceAdapter ?? throw new ArgumentNullException(nameof(graphicsDeviceAdapter));
            this.canvasDevice = ((CanvasDeviceAdapter)this.graphicsDeviceAdapter).CanvasDevice;
        }

        public bool CanLoadResource(string uri)
        {
            throw new NotImplementedException();
        }

        public async Task<ITexture> LoadResourceAsync(string uri)
        {
            CanvasBitmap bitmap = await CanvasBitmap.LoadAsync(this.canvasDevice, new Uri(uri));
            return new Texture(bitmap);
        }
    }
}
