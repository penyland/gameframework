// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using Windows.System;
using Windows.UI.Core;

namespace GameFramework.Platform.Input
{
    public class CoreWindowKeyboardInputSource : IKeyboardInputSource
    {
        private readonly IKeyboard keyboard;
        private readonly IPlatformWindow window;

        public CoreWindowKeyboardInputSource(IPlatformWindow window, IKeyboard keyboard)
        {
            this.window = window;
            this.keyboard = keyboard;
        }

        /// <summary>
        /// Gets a value indicating whether a device identifying itself as a keyboard is detected.
        /// </summary>
        public bool IsConnected
        {
            get
            {
                var capabilities = new Windows.Devices.Input.KeyboardCapabilities();
                return capabilities.KeyboardPresent != 0;
            }
        }

        public void Initialize()
        {
            var coreWindow = (CoreWindow)this.window.Window;
            coreWindow.KeyDown += this.CoreWindow_KeyDown;
            coreWindow.KeyUp += this.CoreWindow_KeyUp;

            this.window.Activated += this.Window_Activated;
        }

        public void Reset()
        {
            throw new System.NotImplementedException();
        }

        public void Update()
        {
            throw new System.NotImplementedException();
        }

        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            this.HandleKey(false, args.VirtualKey, args.KeyStatus);
        }

        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            this.HandleKey(true, args.VirtualKey, args.KeyStatus);
        }

        private void Window_Activated(object sender, System.EventArgs e)
        {
            this.keyboard.Reset();
        }

        private void HandleKey(bool isDown, VirtualKey virtualKey, CorePhysicalKeyStatus status)
        {
            switch (virtualKey)
            {
                case VirtualKey.Control:
                    virtualKey = status.IsExtendedKey ? VirtualKey.RightControl : VirtualKey.LeftControl;
                    break;
                case VirtualKey.Menu:
                    virtualKey = status.IsExtendedKey ? VirtualKey.RightMenu : VirtualKey.LeftMenu;
                    break;
                case VirtualKey.Shift:
                    virtualKey = status.ScanCode == 0x36 ? VirtualKey.RightShift : VirtualKey.LeftShift;
                    break;
            }

            var key = (Keys)virtualKey;

            if (isDown)
            {
                this.KeyDown(key);
            }
            else
            {
                this.KeyUp(key);
            }
        }

        private void KeyUp(Keys key)
        {
            this.keyboard.OnKeyUp(key);
        }

        private void KeyDown(Keys key)
        {
            this.keyboard.OnKeyDown(key);
        }
    }
}
