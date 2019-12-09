// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;

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
