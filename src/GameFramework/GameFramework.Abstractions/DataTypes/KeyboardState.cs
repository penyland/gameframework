// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;

namespace GameFramework.Abstractions
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

        public void SetKey(Keys key)
        {
            this.PressedKeys.Add(key);
        }

        public void ClearKey(Keys key)
        {
            this.PressedKeys.Remove(key);
            this.ReleasedKeys.Add(key);
        }

        public void Clear()
        {
            this.PressedKeys.Clear();
            this.ReleasedKeys.Clear();
        }
    }
}
