// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Hosting.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace GameFramework.Hosting
{
    public static class GameHostBuilderExtensions
    {
        public static IGameHostBuilder UseStartup(this IGameHostBuilder hostBuilder, Type startupType)
        {
            var startupAssemblyName = startupType.GetTypeInfo().Assembly.GetName().Name;

            return hostBuilder;
        }

        /// <summary>
        /// Specify the startup type to be used by the game host.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <typeparam name ="TStartup">The type containing the startup methods for the application.</typeparam>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        public static IGameHostBuilder UseStartup<TStartup>(this IGameHostBuilder hostBuilder)
            where TStartup : class
        {
            return hostBuilder.UseStartup(typeof(TStartup));
        }

        /// <summary>
        /// Adds a delegate for configuring the <see cref="IConfigurationBuilder"/> that will construct an <see cref="IConfiguration"/>.
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IGameHostBuilder"/> to configure.</param>
        /// <param name="configureDelegate">The delegate for configuring the <see cref="IConfigurationBuilder" /> that will be used to construct an <see cref="IConfiguration" />.</param>
        /// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        /// <remarks>
        /// The <see cref="IConfiguration"/> and <see cref="ILoggerFactory"/> on the <see cref="WebHostBuilderContext"/> are uninitialized at this stage.
        /// The <see cref="IConfigurationBuilder"/> is pre-populated with the settings of the <see cref="IGameHostBuilder"/>.
        /// </remarks>
        //public static IGameHostBuilder ConfigureAppConfiguration(this IGameHostBuilder hostBuilder, Action<IConfigurationBuilder> configureDelegate)
        //{
        //    return hostBuilder.ConfigureAppConfiguration((context, builder) => configureDelegate(builder));
        //}

        ///// <summary>
        ///// Adds a delegate for configuring the provided <see cref="ILoggingBuilder"/>. This may be called multiple times.
        ///// </summary>
        ///// <param name="hostBuilder">The <see cref="IGameHostBuilder" /> to configure.</param>
        ///// <param name="configureLogging">The delegate that configures the <see cref="ILoggingBuilder"/>.</param>
        ///// <returns>The <see cref="IGameHostBuilder"/>.</returns>
        //public static IGameHostBuilder ConfigureLogging(this IGameHostBuilder hostBuilder, Action<ILoggingBuilder> configureLogging)
        //{
        //    return hostBuilder.ConfigureServices(collection => collection.AddLogging(configureLogging));
        //}
    }
}
