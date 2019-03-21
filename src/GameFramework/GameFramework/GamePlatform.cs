// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Platform;
using GameFramework.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace GameFramework
{
    public class GamePlatform<T>
        where T : IGameFactory, new()
    {
        private readonly IGameFactory gameFactory;
        private readonly IPlatformFactory platformFactory;
        private readonly IServiceCollection serviceCollection = new ServiceCollection();
        private readonly IPlatformWindow window;

        private GamePlatform(IPlatformWindow window, IPlatformFactory platformFactory)
        {
            this.gameFactory = new T();
            this.platformFactory = platformFactory;
            this.window = window;

            this.ConfigureServices(this.serviceCollection);
        }

        public IServiceProvider Services { get; private set; }

        public static GamePlatform<T> Create(IPlatformWindow window, IPlatformFactory platformFactory)
        {
            return new GamePlatform<T>(window, platformFactory);
        }

        public void Initialize()
        {
            this.platformFactory.RegisterServices(this.serviceCollection);
        }

        public void Suspend()
        {
        }

        public void Resume()
        {
        }

        public void Run()
        {
            // All framework and platform services are added to the container, build it.
            this.BuildServiceProvider(this.serviceCollection);

            // Start running the framework
            this.Services.GetService<IGameWindow>().Run();
        }

        // Create service provider
        protected virtual void ConfigureServices(IServiceCollection services)
        {
            // Build config
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton<IConfiguration>(configuration);

            // Add logging
            services.AddLogging(configure =>
                configure
                    .AddDebug()
                    .AddConsole());

            services.AddOptions();

            // Add platform window
            services.AddSingleton<IPlatformWindow>(this.window);

            this.platformFactory.RegisterServices(services);

            // Add game services
            this.gameFactory.AddServices(services);

            // Add framework required services
            services.AddSingleton<IGameWindow, GameWindow>();
            services.AddSingleton<IResourceManager, ResourceManager>();
            services.AddSingleton<IGraphicsDevice, GraphicsDevice>();
        }

        private void BuildServiceProvider(IServiceCollection services)
        {
            this.Services = services.BuildServiceProvider();
        }
    }
}
