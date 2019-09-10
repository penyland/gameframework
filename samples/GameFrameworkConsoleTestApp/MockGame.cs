// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework;
using GameFramework.Contracts;
using GameFramework.Graphics;
using System;
using System.Threading.Tasks;

namespace GameFrameworkConsoleTestApp
{
    internal class MockGame : IGame
    {
        public event EventHandler OnCompleted;

        public void BeginDraw()
        {
            throw new NotImplementedException();
        }

        public Task CreateResourcesAsync()
        {
            Console.WriteLine("MockGame.CreateResourcesAsync()");

            Task test = new Task(() => { }).ContinueWith((task) => this.OnCompleted(this, null));

            return Task.FromResult<object>(null);
        }

        public void Draw(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public void Draw(GameTime gameTime, IDrawingSession drawingSession)
        {
            throw new NotImplementedException();
        }

        public void EndDraw()
        {
            throw new NotImplementedException();
        }

        public void Initialize()
        {
            Console.WriteLine("MockGame.Initialize()");
        }

        public void Update(GameTime gameTime)
        {
            Console.WriteLine($"MockGame.Update ElapsedGameTime ={gameTime.ElapsedGameTime}");
            Console.WriteLine($"MockGame.Update TotalGameTime   ={gameTime.TotalGameTime}");
            Console.WriteLine($"MockGame.Update UpdateCount     ={gameTime.UpdateCount}");
            Console.WriteLine($"MockGame.Update IsRunningSlowly ={gameTime.IsRunningSlowly}");

            var colors = Colors.CornflowerBlue.ToVector4();
            var c1 = new Color(0x11223344);
            var c2 = new Color(0x44332211);

            var c11 = c1.ToVector4();
            var c21 = c2.ToVector4();
        }
    }
}
