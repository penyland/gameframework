// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Hosting.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace GameFramework.Hosting
{
    /// <summary>
    /// Extension methods for configuring the IGameHostBuilder.
    /// </summary>
    public static class GenericHostBuilderExtensions
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IGameHostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the <see cref="IGameHostBuilder"/>:
        ///     use Kestrel as the web server and configure it using the application's configuration providers,
        ///     adds the HostFiltering middleware,
        ///     adds the ForwardedHeaders middleware if ASPNETCORE_FORWARDEDHEADERS_ENABLED=true,
        ///     and enable IIS integration.
        /// </remarks>
        /// <param name="builder">The <see cref="IHostBuilder" /> instance to configure.</param>
        /// <param name="configure">The configure callback.</param>
        /// <returns>The <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureGameHostDefaults(this IHostBuilder builder, Action<IGameHostBuilder> configure)
        {
            return builder.ConfigureGameHost(gameHostBuilder =>
            {
                GameFrameworkHost.ConfigureGameFrameworkDefaults(gameHostBuilder);

                configure(gameHostBuilder);
            });
        }

        public static IHostBuilder UseGame<T>(this IHostBuilder builder)
            where T : IGame, new()
        {
            return builder.ConfigureServices(services =>
            {
                services.AddSingleton<IGame>(new T());
            });
        }
    }
}
