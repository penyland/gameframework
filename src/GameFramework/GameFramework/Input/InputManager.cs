// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Core;

namespace GameFramework.Input
{
    public class InputManager : GameComponentBase, IGameComponent, IInputManager
    {
        private readonly IKeyboard keyboard;
        private readonly IKeyboardInputSource keyboardDeviceAdapter;

        // Maintain list of connected input devices that the framework can use
        // such as keyboard, mouse, gamepads, accelerometer, orientation sensor etc

        // GamePad
        // Supports gamepad?

        // Touch

        // Mouse
        public InputManager(IKeyboard keyboard, IKeyboardInputSource keyboardInputSource)
        {
            this.keyboard = keyboard;
            this.keyboardDeviceAdapter = keyboardInputSource;
        }

        public bool HasKeyboard => this.keyboard != null;

        public IKeyboard Keyboard => this.keyboard;

        public override void Update(GameTime gameTime)
        {
            // Update all input sources so they can send events and update their state
            this.keyboard.Update();
        }

        public void Update()
        {
            this.keyboard.Update();
        }

        public override void Initialize()
        {
            this.keyboardDeviceAdapter.Initialize();
        }
    }
}
