// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace GameFramework.Resources
{
    public sealed class ResourceManager : IResourceManager
    {
        // Register all content readers in the container?
        private readonly Dictionary<string, IResourceLoader> resourceLoaders = new Dictionary<string, IResourceLoader>();
        private readonly Dictionary<string, object> resources = new Dictionary<string, object>();
        private readonly IGraphicsDevice graphicsDevice;

        public ResourceManager(IGraphicsDevice graphicsDevice, ITextureResourceLoader textureResourceLoader)
        {
            this.graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            //this.ResourceCreator = resourceCreator ?? throw new ArgumentNullException(nameof(resourceCreator));
            this.resourceLoaders.Add("texture", textureResourceLoader);
        }

        public void AddResourceLoader(IResourceLoader resourceLoader)
        {
            if (!this.resourceLoaders.ContainsKey("png"))
            {
                this.resourceLoaders.Add("png", resourceLoader);
            }
        }

        //public async Task<IResource> LoadAsync(string uri)
        //{
        //    // Check type
        //    Uri resource = new Uri(uri);
        //    string fileExtension = Path.GetExtension(uri);

        //    IResourceLoader resourceLoader = this.resourceLoaders[fileExtension];

        //    // Ask all content readers if they can read a certain type then use it otherwise throw exception
        //    // If no extension read from xnb
        //    // otherwise find a resource reader for the type of content
        //    return await resourceLoader.LoadResourceAsync(uri);
        //}

        public async Task<T> LoadAsync<T>(string uri)
        {
            if (this.resources.TryGetValue(uri, out object result))
            {
                if (result is T t)
                {
                    return t;
                }
            }

            result = await this.LoadResourceAsync<T>(uri);
            this.resources[uri] = result;
            return (T)result;
        }

        private async Task<T> LoadResourceAsync<T>(string uri)
        {
            object result = null;

            // Resolve T
            //if (typeof(T) is ITexture)
            //{
            var resLoader = (ITextureResourceLoader)this.resourceLoaders["texture"];
            result = await resLoader.LoadResourceAsync(uri);
            //}

            return (T)result;
        }

        private IResourceLoader ResolveResourceLoader(string key)
        {
            return this.resourceLoaders[key];
        }

        private void CanLoadResource()
        {
        }
    }
}
