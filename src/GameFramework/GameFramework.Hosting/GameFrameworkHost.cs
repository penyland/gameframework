// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Hosting.Abstractions;

namespace GameFramework.Hosting
{
    public static class GameFrameworkHost
    {
        public static void ConfigureGameFrameworkDefaults(IGameHostBuilder builder)
        {
            builder.ConfigureAppConfiguration((context, cb) =>
            {
                if (context.HostingEnvironment.EnvironmentName == "Development")
                {
                }
            })
            .ConfigureFramework();
        }
    }
}
