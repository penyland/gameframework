// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Contracts
{
    /// <summary>
    /// Container for registered input devices such as keyboard, touch, gamepad and mouse
    /// </summary>
    public interface IInputManager
    {
        void Update(GameTime gameTime);
        void Update();
    }
}
