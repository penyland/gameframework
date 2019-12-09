// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Abstractions
{
    public interface IGameLoopClient
    {
        void OnGameLoopStarting();

        void OnGameLoopStopped();

        bool Tick(ISwapChain swapChain, bool areResourcesCreated);

        void OnTickLoopEnded();
    }
}
