// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Graphics;
using GameFramework.Hosting.Abstractions;
using GameFramework.Input;
using GameFramework.Resources;
using Microsoft.Extensions.DependencyInjection;

namespace GameFramework.Hosting
{
    public static class GameHostBuilderExtensions
    {
        public static IGameHostBuilder ConfigureFramework(this IGameHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices(services =>
            {
                services.AddSingleton<IResourceManager, ResourceManager>();
                services.AddSingleton<IGraphicsDevice, GraphicsDevice>();
                services.AddSingleton<IGameWindow, GameWindow>();
                services.AddSingleton<IKeyboard, Keyboard>();
                services.AddSingleton<IInputManager, InputManager>();
                services.AddSingleton<IGamePlatform, GamePlatform>();
            });

            return hostBuilder;
        }
    }
}
