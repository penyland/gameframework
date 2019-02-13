// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;

namespace Win2D.UWPCore
{
    public class GamepadManager
    {
        private List<Gamepad> gamepads;

        private readonly object mutex = new object();

        public GamepadManager()
        {
            this.gamepads = new List<Gamepad>();

            Gamepad.GamepadAdded += this.Gamepad_GamepadAdded;
            Gamepad.GamepadRemoved += this.Gamepad_GamepadRemoved;

            foreach (var gamepad in Gamepad.Gamepads)
            {
                this.Gamepad_GamepadAdded(null, gamepad);
            }
        }

        public Gamepad GetFirstGamepad()
        {
            lock (this.mutex)
            {
                return this.gamepads.FirstOrDefault();
            }
        }

        private void Gamepad_GamepadRemoved(object sender, Gamepad e)
        {
            lock (this.mutex)
            {
                this.gamepads.Remove(e);
            }
        }

        private void Gamepad_GamepadAdded(object sender, Gamepad e)
        {
            lock (this.mutex)
            {
                bool exists = this.gamepads.Exists(t => t == e);
                if (!exists)
                {
                    this.gamepads.Add(e);
                }
            }
        }
    }
}
