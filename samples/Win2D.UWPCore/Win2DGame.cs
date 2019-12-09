// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework;
using GameFramework.Abstractions;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;

namespace Win2D.UWPCore
{
    public class Win2DGame : Game
    {
        private readonly IKeyboard keyboard;

        private ITexture particleBitmap;
        private ITexture maxBitmap;

        public Win2DGame(
            IConfiguration configuration,
            IGraphicsDevice graphicsDevice,
            IResourceManager resourceManager,
            IInputManager inputManager)
            : base(configuration, graphicsDevice, resourceManager, inputManager)
        {
            if (inputManager.HasKeyboard)
            {
                this.keyboard = inputManager.Keyboard;
            }
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
            await base.CreateResourcesAsync();

            Debug.WriteLine("Win2DGame.CreateResourcesAsync()");

            this.particleBitmap = await this.ResourceManager.LoadAsync<ITexture>("ms-appx:///Assets/Particle.png");
            this.maxBitmap = await this.ResourceManager.LoadAsync<ITexture>("ms-appx:///Assets/max_face_south.png");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            using (var ds = this.GraphicsDevice.CreateDrawingSession())
            {
                if (gameTime.TotalGameTime.Seconds >= 2)
                {
                    ds.Clear(Colors.AliceBlue);
                }

                if (gameTime.TotalGameTime.Seconds >= 4)
                {
                    ds.Clear(Colors.AntiqueWhite);
                }

                if (gameTime.TotalGameTime.Seconds >= 6)
                {
                    ds.Clear(Colors.Aqua);
                }

                if (gameTime.TotalGameTime.Seconds >= 8)
                {
                    ds.Clear(Colors.Aquamarine);
                }

                if (gameTime.TotalGameTime.Seconds >= 10)
                {
                    ds.Clear(Colors.CornflowerBlue);
                }

                ds.DrawText("Hello GameFramework", new Vector2(0, 0), Colors.Black);

                ds.DrawText($"GameTime.ElapsedGameTime = {gameTime.ElapsedGameTime.TotalMilliseconds}", new Vector2(0, 40), Colors.Black);
                ds.DrawText($"GameTime.TotalGameTime   = {gameTime.TotalGameTime}", new Vector2(0, 60), Colors.Black);
                ds.DrawText($"GameTime.UpdateCount     = {gameTime.UpdateCount}", new Vector2(0, 80), Colors.Black);
                ds.DrawText($"GameTime.IsRunningSlowly = {gameTime.IsRunningSlowly}", new Vector2(0, 100), Colors.Black);

                float frameRate = 1 / (float)gameTime.ElapsedGameTime.TotalSeconds;
                ds.DrawText($"FrameRate = {frameRate}", new Vector2(0, 120), Colors.Black);

                ds.DrawText("LogicalDPI = " + this.GraphicsDevice.LogicalDpi, new Vector2(0, 160), Colors.Black);
                ds.DrawText("Size       = " + this.GraphicsDevice.Size.ToString(), new Vector2(0, 180), Colors.Black);

                var matrix3X2 = Matrix3x2.CreateTranslation(100, 100);

                ds.Draw(this.maxBitmap, matrix3X2, Vector4.One);

                KeyboardState keyBoardState = this.keyboard.GetState();

                if (keyBoardState.PressedKeys.Count > 0)
                {
                    ds.DrawText("KeyboardState: " + keyBoardState.PressedKeys.Count + " keys pressed", new Vector2(10, 300), Colors.Black);

                    var keyList = keyBoardState.PressedKeys.ToList();
                    for (int i = 0; i < keyList.Count; i++)
                    {
                        ds.DrawText("KeyboardState: " + keyList[i].ToString() + " pressed", new Vector2(10, 315 + (i * 15)), Colors.Black);
                    }
                }

                // this.spriteBatch.Begin();
                // this.spriteBatch.Draw();
            }
        }
    }
}
