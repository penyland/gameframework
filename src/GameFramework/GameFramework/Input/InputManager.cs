// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Core;

namespace GameFramework.Input
{
    internal class InputManager : GameComponentBase, IGameComponent, IInputManager
    {
        private readonly IKeyboard keyboard;

        // Maintain list of connected input devices that the framework can use
        // such as keyboard, mouse, gamepads, accelerometer, orientation sensor etc

        // GamePad
        // Supports gamepad?

        // Touch

        // Mouse

        public InputManager(IKeyboard keyboard)
        {
            this.keyboard = keyboard;
        }

        public bool HasKeyboard => this.keyboard != null;

        public override void Update(GameTime gameTime)
        {
            // Update all input sources so they can send events and update their state
            this.keyboard.Update();
        }

        public void Update()
        {
            this.keyboard.Update();
        }
    }
}
