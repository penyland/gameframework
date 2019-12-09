// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using System;

namespace GameFramework.Hosting
{
    internal class GenericGameHostBuilder : IGameHostBuilder
    {
        private readonly IHostBuilder builder;
        private readonly IConfiguration config;

        public GenericGameHostBuilder(IHostBuilder builder)
        {
            this.builder = builder;
            this.config = new ConfigurationBuilder()
                .AddEnvironmentVariables(prefix: "GAMEFRAMEWORK_")
                .Build();

            this.builder.ConfigureHostConfiguration(config =>
            {
                config.AddConfiguration(this.config);
            });

            this.builder.ConfigureAppConfiguration((context, builder) =>
            {
                // Get context
            });

            this.builder.ConfigureServices((context, services) =>
            {
                // Get context
                // Add services
            });
        }

        public IGameHostBuilder ConfigureServices(Action<IServiceCollection> configureServices)
        {
            return this.ConfigureServices((context, services) => configureServices(services));
        }

        public IGameHostBuilder ConfigureAppConfiguration(Action<GameHostBuilderContext, IConfigurationBuilder> configureDelegate)
        {
            this.builder.ConfigureAppConfiguration((context, builder) =>
            {
                var gameHostBuilderContext = this.GetGameHostBuilderContext(context);
                configureDelegate(gameHostBuilderContext, builder);
            });

            return this;
        }

        public IGameHostBuilder ConfigureServices(Action<GameHostBuilderContext, IServiceCollection> configureServices)
        {
            this.builder.ConfigureServices((context, builder) =>
            {
                var gameHostBuilderContext = this.GetGameHostBuilderContext(context);
                configureServices(gameHostBuilderContext, builder);
            });

            return this;
        }

        private GameHostBuilderContext GetGameHostBuilderContext(HostBuilderContext context)
        {
            if (!context.Properties.TryGetValue(typeof(GameHostBuilderContext), out var contextVal))
            {
                var gameHostBuilderContext = new GameHostBuilderContext
                {
                    Configuration = context.Configuration,
                    HostingEnvironment = new HostingEnvironment(),
                };

                // gameHostBuilderContext.HostingEnvironment.Initialize(context.HostingEnvironment.ContentRootPath, null);
                context.Properties[typeof(GameHostBuilderContext)] = gameHostBuilderContext;
                return gameHostBuilderContext;
            }

            var gamehostBuilderContext = (GameHostBuilderContext)contextVal;
            gamehostBuilderContext.Configuration = context.Configuration;
            return gamehostBuilderContext;
        }
    }
}
