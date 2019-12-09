// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GameFramework.Hosting.Abstractions
{
    public interface IGameHostBuilder
    {
        IGameHostBuilder ConfigureAppConfiguration(Action<GameHostBuilderContext, IConfigurationBuilder> configureDelegate);

        IGameHostBuilder ConfigureServices(Action<IServiceCollection> configureServices);

        IGameHostBuilder ConfigureServices(Action<GameHostBuilderContext, IServiceCollection> configureServices);
    }
}
