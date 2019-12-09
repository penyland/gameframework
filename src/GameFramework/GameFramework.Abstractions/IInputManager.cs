// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Abstractions
{
    /// <summary>
    /// Container for registered input devices such as keyboard, touch, gamepad and mouse.
    /// </summary>
    public interface IInputManager
    {
        bool HasKeyboard { get; }

        IKeyboard Keyboard { get; }

        void Update(GameTime gameTime);

        void Update();

        void Initialize();
    }
}
