// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;

namespace GameFramework
{
    public interface IGamePlatform
    {
        IGraphicsDevice GraphicsDevice { get; }

        IInputManager InputManager { get; }

        IResourceManager ResourceManager { get; }
    }
}
