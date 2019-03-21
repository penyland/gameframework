// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graphics.Canvas;

namespace GameFramework.Platform
{
    public sealed class PlatformFactory : IPlatformFactory
    {
        private PlatformFactory()
        {
        }

        // Add all platform specific classes to the application container
        public void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IGraphicsDeviceAdapter, CanvasDeviceAdapter>();
            services.AddSingleton<ISwapChain, CanvasSwapChainAdapter>();
            services.AddSingleton<ITextureResourceLoader, CanvasBitmapResourceLoader>();
        }

        public void RegisterResourceLoaders()
        {
        }

        internal static IPlatformFactory Create()
        {
            return new PlatformFactory();
        }
    }
}
