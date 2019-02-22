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
        // SoundDevice
        // ResourceManagement
        // InputDevices
        public Game(IConfiguration configuration, IGraphicsDevice graphicsDevice)
        {
            this.Configuration = configuration;
            this.GraphicsDevice = graphicsDevice;
        }

        public IServiceProvider Container { get; private set; }

        public IGraphicsDevice GraphicsDevice { get; }

        protected IConfiguration Configuration { get; }

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

        public virtual void Draw(GameTime gameTime)
        {
        }

        public virtual void Draw(GameTime gameTime, IDrawingSession drawingSession)
        {
        }
    }
}
