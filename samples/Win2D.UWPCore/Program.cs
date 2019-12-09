// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Hosting;
using GameFramework.Platform;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Win2D.UWPCore
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args)
                .Build()
                .RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return GameHost.CreateDefaultBuilder(args)
                .ConfigureGameHostDefaults(gameBuilder =>
                {
                    gameBuilder.ConfigurePlatform();
                })
                .ConfigureAppConfiguration((context, builder) =>
                {
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IGame, Win2DGame>();
                })
                .UseEnvironment("Development");
        }
    }
}
