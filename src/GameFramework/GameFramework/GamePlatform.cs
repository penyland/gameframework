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
        private readonly IPlatformWindow platformWindow;
        private readonly IServiceCollection serviceCollection = new ServiceCollection();

        private GamePlatform(IPlatformWindow platformWindow)
        {
            this.platformWindow = platformWindow;
            this.gameFactory = new T();
            this.ConfigureServices(this.serviceCollection);
        }

        public IServiceProvider Services { get; private set; }

        public static GamePlatform<T> Create(IPlatformWindow platformWindow)
        {
            return new GamePlatform<T>(platformWindow);
        }

        public void Activated()
        {
        }

        public void Suspending()
        {
        }

        public void Run()
        {
            // A Game is hosted within a window
            this.Services.GetService<IGameWindow>().Run();
        }

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

            // Add game implementation
            this.gameFactory.AddGame(services);

            // Add framework required services
            services.AddSingleton<IPlatformWindow>(this.platformWindow);
            services.AddSingleton<IGameWindow, GameWindow>();

            // Create service provider
            this.Services = services.BuildServiceProvider();
        }
    }
}
