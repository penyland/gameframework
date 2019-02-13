// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace GameFramework
{
    public class Game : IGame
    {
        // GamePlatform
        // GameWindow
        // GraphicsDevice
        // SoundDevice
        // ResourceManagement
        // InputDevices
        public Game(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IServiceProvider Container { get; private set; }

        protected IConfiguration Configuration { get; }

        public void Run()
        {
            // Initialize
            // LoadContent

            while (true)
            {
                //CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessUntilQuit);

                // Render

                // Update
                // Draw
            }
        }

        public virtual void Initialize()
        {
        }

        public virtual Task CreateResourcesAsync()
        {
            return Task.FromResult<object>(null);
        }

        public virtual void Update(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, IDrawingSession drawingSession)
        {
        }

        internal void Tick()
        {
            // Increment time, call Update and Draw
        }
    }
}
