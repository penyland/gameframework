﻿// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using System.Threading.Tasks;

namespace GameFramework.Core
{
    public abstract class GameComponentBase : IGameComponent
    {
        public virtual void BeginDraw()
        {
        }

        public virtual Task CreateResourcesAsync()
        {
            return Task.CompletedTask;
        }

        public virtual void Dispose()
        {
        }

        public virtual void Draw(GameTime gameTime)
        {
        }

        public virtual void EndDraw()
        {
        }

        public virtual void Initialize()
        {
        }

        public virtual void Update(GameTime gameTime)
        {
        }
    }
}
