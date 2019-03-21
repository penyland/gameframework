// Copyright (c) Peter Nylander.  All rights reserved.

using Microsoft.Extensions.DependencyInjection;

namespace GameFramework.Contracts
{
    public interface IPlatformFactory
    {
        void RegisterServices(IServiceCollection services);
    }
}
