// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework
{
    /// <summary>
    /// This class should be used for IGame implementations to access framework provided services
    /// so each game instance doesn't need to have those injected in the Game constructor.
    /// </summary>
    public class GamePlatform
    {
        public GamePlatform(
            IGraphicsDevice graphicsDevice,
            IResourceManager resourceManager,
            IInputManager inputManager)
        {
            this.GraphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.ResourceManager = resourceManager ?? throw new ArgumentNullException(nameof(resourceManager));
            this.InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
        }

        public IGraphicsDevice GraphicsDevice { get; }

        public IResourceManager ResourceManager { get; }

        public IInputManager InputManager { get; }
    }
}
