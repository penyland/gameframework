using GameFramework.Input;
using System;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var keyboard = new Keyboard();

            keyboard.KeyDown(Keys.A);
            keyboard.KeyUp(Keys.A);
            keyboard.KeyDown(Keys.B);
            keyboard.KeyDown(Keys.Back);
            keyboard.KeyUp(Keys.Back);
            keyboard.KeyUp(Keys.B);

            //keyboard.KeyDown(Keys.A);
        }
    }

    class Keyboard
    {
        public uint state;
        ulong key1;


        public void KeyDown(Keys key)
        {
            uint mask = 1U << (((int)key) & 0x1f);
            int element = (int)key >> 5;

            state |= mask;
        }

        public void KeyUp(Keys key)
        {
            uint mask = 1U << (((int)key) & 0x1f);
            int element = (int)key >> 5;

            state &= ~mask;
        }
    }
}
