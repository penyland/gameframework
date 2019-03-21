// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework;
using System;
using System.Diagnostics;

namespace GameFrameworkConsoleTestApp
{

    internal class Bootstrap
    {
        private GamePlatform<GameFactory> gamePlatform;

        public Bootstrap()
        {
            this.gamePlatform = GamePlatform<GameFactory>.Create(new MockWindow(), null);
        }

        public void Run()
        {
            this.gamePlatform.Run();
        }
    }
}
