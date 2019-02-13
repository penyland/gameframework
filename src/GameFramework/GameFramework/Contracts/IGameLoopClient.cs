// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Contracts
{
    public interface IGameLoopClient
    {
        void OnGameLoopStarting();

        void OnGameLoopStopped();

        bool Tick(ISwapChain swapChain, bool areResourcesCreated);

        void OnTickLoopEnded();
    }
}
