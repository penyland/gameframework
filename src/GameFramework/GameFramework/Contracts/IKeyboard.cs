// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Input;

namespace GameFramework.Contracts
{
    public interface IKeyboard : IInputDevice
    {
        KeyboardState GetState();

        void OnKeyDown(Keys key);

        void OnKeyUp(Keys key);
    }
}
