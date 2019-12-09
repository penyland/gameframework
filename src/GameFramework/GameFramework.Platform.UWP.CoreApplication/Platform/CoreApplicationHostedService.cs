// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Platform.Abstractions;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;

namespace GameFramework.Platform
{
    public class CoreApplicationHostedService : IHostedService
    {
        private readonly IGameFrameworkView gameFrameworkView;
        private readonly ICoreApplicationContext coreApplicationContext;
        private readonly IServiceProvider serviceProvider;

        public CoreApplicationHostedService(ICoreApplicationContext coreApplicationContext, IServiceProvider serviceProvider, IGameFrameworkView gameFrameworkView)
        {
            this.gameFrameworkView = gameFrameworkView ?? throw new ArgumentNullException(nameof(gameFrameworkView));
            this.coreApplicationContext = coreApplicationContext ?? throw new ArgumentNullException(nameof(coreApplicationContext));
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));

            CoreApplication.Exiting += this.CoreApplication_Exiting;
            this.gameFrameworkView.OnActivated += this.GameFrameworkView_OnActivated;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            CoreApplication.Run((IFrameworkViewSource)this.gameFrameworkView);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void GameFrameworkView_OnActivated(object sender, EventArgs e)
        {
            this.gameFrameworkView.Activate();
        }

        private void CoreApplication_Exiting(object sender, object e)
        {
            throw new NotImplementedException();
        }
    }
}
