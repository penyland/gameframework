// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GameFrameworkConsoleTestApp
{
    internal class GameFactory : IGameFactory
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IGame, MockGame>();
        }

        public IGame Create(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<MockGame>();
        }
    }
}
