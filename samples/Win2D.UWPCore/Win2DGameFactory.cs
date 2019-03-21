// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Platform;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Win2D.UWPCore
{
    public class Win2DGameFactory : IGameFactory
    {
        public void AddServices(IServiceCollection services)
        {
            services.AddSingleton<IGame, Win2DGame>();
        }
    }
}
