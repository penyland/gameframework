// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Input;
using GameFramework.Platform.Input;
using Microsoft.Extensions.DependencyInjection;

namespace GameFramework.Platform
{
    public sealed class PlatformFactory : IPlatformFactory
    {
        private PlatformFactory()
        {
        }

        public void RegisterResourceLoaders()
        {
        }

        // Add all platform specific classes to the application container
        public void Initialize(IPlatformWindow window, IServiceCollection services)
        {
            services.AddSingleton<IGraphicsDeviceAdapter, CanvasDeviceAdapter>();
            services.AddSingleton<ISwapChain, CanvasSwapChainAdapter>();
            services.AddSingleton<ITextureResourceLoader, CanvasBitmapResourceLoader>();

            // Initialize devices
            var keyboard = new Keyboard();
            var keyboardDeviceManager = new CoreWindowKeyboardInputSource(window, keyboard);

            services.AddSingleton<IKeyboardDeviceAdapter>(keyboardDeviceManager);
            services.AddSingleton<IKeyboard>(keyboard);
        }

        internal static IPlatformFactory Create()
        {
            return new PlatformFactory();
        }
    }
}
