// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GameFramework
{
    public abstract class GameBase : IGame
    {
        public GameBase()
        {
        }

        public IList<IGameComponent> GameComponents { get; } = new List<IGameComponent>();

        public IServiceProvider Services { get; private set; }

        public GameWindow Window { get; private set; }

        public virtual void Initialize()
        {
            foreach (IGameComponent gameComponent in this.GameComponents)
            {
                gameComponent.Initialize();
            }
        }

        public virtual Task CreateResourcesAsync()
        {
            Debug.WriteLine("GameBase.CreateResourcesAsync()");

            var loadingTasks = new List<Task>(this.GameComponents.Count);

            foreach (IGameComponent gameComponent in this.GameComponents)
            {
                loadingTasks.Add(gameComponent.CreateResourcesAsync());
            }

            return Task.WhenAll(loadingTasks);
        }

        public virtual void Update(GameTime gameTime)
        {
            foreach (IGameComponent gameComponent in this.GameComponents)
            {
                gameComponent.Update(gameTime);
            }
        }

        public virtual void BeginDraw()
        {
            foreach (IGameComponent gameComponent in this.GameComponents)
            {
                gameComponent.BeginDraw();
            }
        }

        public virtual void Draw(GameTime gameTime)
        {
            foreach (IGameComponent gameComponent in this.GameComponents)
            {
                gameComponent.Draw(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, IDrawingSession drawingSession)
        {
        }

        public virtual void EndDraw()
        {
            foreach (IGameComponent gameComponent in this.GameComponents)
            {
                gameComponent.EndDraw();
            }
        }
    }
}
