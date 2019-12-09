// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Hosting;
using GameFramework.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace CoreApplicationTest
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return GameHost.CreateDefaultBuilder(args)
                .ConfigureGameHostDefaults(gameBuilder =>
                {
                    gameBuilder.UseStartup<Window>();

                    gameBuilder.ConfigurePlatform();
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IGame, Game1>();
                })
                .UseEnvironment("Development");
        }
    }
}
