// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Graphics
{
    /// <summary>
    /// Enables a group of sprites to be drawn using the same settings.
    /// </summary>
    public class SpriteBatch : IDisposable
    {
        public SpriteBatch(IGraphicsDevice graphicsDevice)
        {
            this.GraphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
        }

        public IGraphicsDevice GraphicsDevice { get; }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
