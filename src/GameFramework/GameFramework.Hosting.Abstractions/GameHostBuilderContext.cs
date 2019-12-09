// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GameFramework.Hosting.Abstractions
{
    public class GameHostBuilderContext
    {
        /// <summary>
        /// Gets or sets the <see cref="IHostEnvironment" /> initialized by the <see cref="IGameHost" />.
        /// </summary>
        public IHostEnvironment HostingEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="IConfiguration" /> containing the merged configuration of the application and the <see cref="IGameHost" />.
        /// </summary>
        public IConfiguration Configuration { get; set; }
    }
}
