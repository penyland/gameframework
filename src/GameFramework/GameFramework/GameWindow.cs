// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using System;
using System.Diagnostics;

namespace GameFramework
{
    public class GameWindow : IGameWindow
    {
        private readonly IPlatformWindow window;
        private readonly IGame game;
        private readonly GameTime gameTime = new GameTime();

        private TimeSpan elapsedTime;
        private Stopwatch gameTimer;
        private long previousTicks = 0;

        public GameWindow(IPlatformWindow platformWindow, IGame game)
        {
            this.window = platformWindow ?? throw new ArgumentNullException(nameof(platformWindow));
            this.game = game ?? throw new ArgumentNullException(nameof(game));
        }

        public void OnActivated()
        {
            throw new System.NotImplementedException();
        }

        public void Run()
        {
            this.game.Initialize();
            this.game.CreateResourcesAsync();

            this.gameTimer = Stopwatch.StartNew();

            while (true)
            {
                this.window.ProcessEvents();

                this.Tick();

                // Update
                this.game.Update(this.gameTime);

                // Draw
                this.game.Draw(this.gameTime, this.window.DrawingSession);

                this.window.Draw();
            }
        }

        public void Tick()
        {
            long currentTicks = this.gameTimer.ElapsedTicks;
            this.elapsedTime += TimeSpan.FromTicks(currentTicks - this.previousTicks);
            this.previousTicks = currentTicks;

            this.gameTime.ElapsedGameTime = this.elapsedTime;
            this.gameTime.TotalGameTime += this.elapsedTime;
            this.elapsedTime = TimeSpan.Zero;
        }
    }
}
