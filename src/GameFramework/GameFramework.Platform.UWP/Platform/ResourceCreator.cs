// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Graphics.Canvas;
using System;
using System.Threading.Tasks;

namespace GameFramework.Platform
{
    public class ResourceCreator : IResourceCreator
    {
        private readonly ICanvasResourceCreator resourceCreator;

        public ResourceCreator(ICanvasResourceCreator canvasResourceCreator)
        {
            this.resourceCreator = canvasResourceCreator ?? throw new ArgumentNullException(nameof(canvasResourceCreator));
        }

        public async Task<ITexture> CreateResourcesAsync(string uri)
        {
            CanvasBitmap bitmap = await CanvasBitmap.LoadAsync(this.resourceCreator, new Uri(uri));
            return new Texture(bitmap);
        }
    }
}
