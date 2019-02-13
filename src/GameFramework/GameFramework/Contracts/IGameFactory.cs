// Copyright (c) Peter Nylander.  All rights reserved.

using Microsoft.Extensions.DependencyInjection;
using System;

namespace GameFramework.Contracts
{
    public interface IGameFactory
    {
        IGame Create(IServiceProvider serviceProvider);

        void AddGame(IServiceCollection services);
    }
}
