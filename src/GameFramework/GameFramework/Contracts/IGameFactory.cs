// Copyright (c) Peter Nylander.  All rights reserved.

using Microsoft.Extensions.DependencyInjection;
using System;

namespace GameFramework.Contracts
{
    public interface IGameFactory
    {
        void AddServices(IServiceCollection services);
    }
}
