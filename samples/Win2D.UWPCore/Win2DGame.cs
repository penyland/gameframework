// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework;
using GameFramework.Contracts;
using GameFramework.Graphics;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;

namespace Win2D.UWPCore
{
    public class Win2DGame : Game
    {
        private ITexture particleBitmap;
        private ITexture maxBitmap;

        public Win2DGame(
            IConfiguration configuration,
            IGraphicsDevice graphicsDevice,
            IResourceManager resourceManager)
            : base(configuration, graphicsDevice, resourceManager)
        {
        }

        public override void Initialize()
        {
            Debug.WriteLine("Win2DGame.Initialize()");

            base.Initialize();

            // Set preferred size of window
            // this.GraphicsDevice.Size

            // ISpriteBatch spriteBatch = new SpriteBatch(this.GraphicsDevice);
        }

        public override async Task CreateResourcesAsync()
        {
            Debug.WriteLine("Win2DGame.CreateResourcesAsync()");

            this.particleBitmap = await this.ResourceManager.LoadAsync<ITexture>("ms-appx:///Assets/Particle.png");
            this.maxBitmap = await this.ResourceManager.LoadAsync<ITexture>("ms-appx:///Assets/max_face_south.png");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime, IDrawingSession drawingSession)
        {
            if (gameTime.TotalGameTime.Seconds >= 2)
            {
                drawingSession.Clear(Colors.AliceBlue);
            }

            if (gameTime.TotalGameTime.Seconds >= 4)
            {
                drawingSession.Clear(Colors.AntiqueWhite);
            }

            if (gameTime.TotalGameTime.Seconds >= 6)
            {
                drawingSession.Clear(Colors.Aqua);
            }

            if (gameTime.TotalGameTime.Seconds >= 8)
            {
                drawingSession.Clear(Colors.Aquamarine);
            }

            if (gameTime.TotalGameTime.Seconds >= 10)
            {
                drawingSession.Clear(Colors.CornflowerBlue);
            }

            drawingSession.DrawText("Hello GameFramework", new Vector2(0, 0), Colors.Black);

            drawingSession.DrawText($"GameTime.ElapsedGameTime = {gameTime.ElapsedGameTime.TotalMilliseconds}", new Vector2(0, 40), Colors.Black);
            drawingSession.DrawText($"GameTime.TotalGameTime   = {gameTime.TotalGameTime}", new Vector2(0, 60), Colors.Black);
            drawingSession.DrawText($"GameTime.UpdateCount     = {gameTime.UpdateCount}", new Vector2(0, 80), Colors.Black);
            drawingSession.DrawText($"GameTime.IsRunningSlowly = {gameTime.IsRunningSlowly}", new Vector2(0, 100), Colors.Black);

            float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
            drawingSession.DrawText($"FrameRate = {frameRate}", new Vector2(0, 120), Colors.Black);

            drawingSession.DrawText("LogicalDPI = " + this.GraphicsDevice.LogicalDpi, new Vector2(0, 160), Colors.Black);
            drawingSession.DrawText("Size       = " + this.GraphicsDevice.Size.ToString(), new Vector2(0, 180), Colors.Black);

            Matrix3x2 matrix3X2 = Matrix3x2.CreateTranslation(100, 100);

            drawingSession.Draw(this.maxBitmap, matrix3X2, Vector4.One);

            base.Draw(gameTime);

            // this.spriteBatch.Begin();
            // this.spriteBatch.Draw();
        }
    }
}
