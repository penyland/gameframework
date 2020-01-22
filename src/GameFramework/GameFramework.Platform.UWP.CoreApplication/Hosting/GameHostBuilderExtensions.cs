// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Hosting.Abstractions;
using GameFramework.Platform.Abstractions;
using GameFramework.Platform.Graphics;
using GameFramework.Platform.Input;
using Microsoft.Extensions.DependencyInjection;

namespace GameFramework.Platform
{
    public static class GameHostBuilderExtensions
    {
        public static IGameHostBuilder ConfigurePlatform(this IGameHostBuilder hostBuilder)
        {
            hostBuilder
                .ConfigureCoreApplication()
                .ConfigurePlatformGraphics();

            return hostBuilder;
        }

        public static IGameHostBuilder ConfigureCoreApplication(this IGameHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IGameFrameworkView, GameFrameworkView>();
                services.AddSingleton<ICoreApplicationContext, CoreApplicationContext>();
                services.AddSingleton<IPlatformWindow, CoreWindowAdapter>();
                services.AddSingleton<IPlatformRenderTargetFactory, CanvasRenderTargetFactory>();
                services.AddSingleton<IKeyboardInputSource, CoreWindowKeyboardInputSource>();

                services.AddHostedService<CoreApplicationHostedService>();
            });

            return hostBuilder;
        }

        public static IGameHostBuilder ConfigurePlatformGraphics(this IGameHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IGraphicsDeviceAdapter, CanvasDeviceAdapter>();
                services.AddSingleton<ISwapChain, CanvasSwapChainAdapter>();
                services.AddSingleton<ITextureResourceLoader, CanvasBitmapResourceLoader>();
            });

            return hostBuilder;
        }
    }
}
