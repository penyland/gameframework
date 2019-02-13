// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Core
{
    public class GameLoop
    {
        private readonly IGameLoopClient gameLoopClient;

        public GameLoop(IGameLoopClient gameLoopClient)
        {
            this.gameLoopClient = gameLoopClient;
        }

        private void Tick()
        {
            this.gameLoopClient.Tick(null, true);
        }
    }
}
