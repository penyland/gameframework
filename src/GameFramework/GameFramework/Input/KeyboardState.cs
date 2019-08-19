// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Collections.Generic;

namespace GameFramework.Input
{
    public class KeyboardState
    {
        public HashSet<Keys> PressedKeys { get; } = new HashSet<Keys>();

        public HashSet<Keys> ReleasedKeys { get; } = new HashSet<Keys>();

        public bool IsKeyDown(Keys key)
        {
            return this.PressedKeys.Contains(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return !this.PressedKeys.Contains(key);
        }

        internal void SetKey(Keys key)
        {
            this.PressedKeys.Add(key);
        }

        internal void ClearKey(Keys key)
        {
            this.PressedKeys.Remove(key);
            this.ReleasedKeys.Add(key);
        }

        internal void Clear()
        {
            this.PressedKeys.Clear();
            this.ReleasedKeys.Clear();
        }
    }
}
