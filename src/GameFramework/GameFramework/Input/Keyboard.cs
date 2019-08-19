// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;

namespace GameFramework.Input
{
    public class Keyboard : IKeyboard
    {
        private KeyboardState currentState = new KeyboardState();
        private KeyboardState nextState = new KeyboardState();

        public Keyboard()
        {
        }

        public bool IsConnected => true;

        public KeyboardState GetState()
        {
            return this.currentState;
        }

        public void OnKeyDown(Keys key)
        {
            this.nextState.SetKey(key);
        }

        public void OnKeyUp(Keys key)
        {
            this.nextState.ClearKey(key);
        }

        public void Update()
        {
            // Swap states
            this.currentState = this.nextState;
        }

        public void Reset()
        {
            this.currentState.Clear();
            this.nextState.Clear();
        }
    }
}
