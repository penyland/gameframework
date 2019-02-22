// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
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
        private readonly IServiceCollection serviceCollection = new ServiceCollection();
        private readonly IPlatformWindow window;

        private GamePlatform(IPlatformWindow window)
        {
            this.gameFactory = new T();
            this.window = window;

            this.ConfigureServices(this.serviceCollection);
        }

        public IServiceProvider Services { get; private set; }

        public static GamePlatform<T> Create(IPlatformWindow window)
        {
            return new GamePlatform<T>(window);
        }

        public void Activate()
        {
            this.BuildServiceProvider(this.serviceCollection);
        }

        public void Suspend()
        {
        }

        public void Resume()
        {
        }

        public void Run()
        {
            // A Game is hosted within a window
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

            // Add game services
            this.gameFactory.AddServices(services);

            // Add framework required services
            services.AddSingleton<IGameWindow, GameWindow>();
        }

        private void BuildServiceProvider(IServiceCollection services)
        {
            this.Services = services.BuildServiceProvider();
        }
    }
}
