// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Hosting.Abstractions;
using Microsoft.Extensions.Hosting;
using System;

namespace GameFramework.Hosting
{
    public static class GenericHostGameHostBuilderExtensions
    {
        public static IHostBuilder ConfigureGameHost(this IHostBuilder builder, Action<IGameHostBuilder> configure)
        {
            var gameHostBuilder = new GenericGameHostBuilder(builder);
            configure(gameHostBuilder);
            return builder;
        }
    }
}
