// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Extensions;
using GameFramework.Graphics;
using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;

namespace GameFramework.Platform
{
    public class DrawingSessionAdapter : IDrawingSession
    {
        private CanvasDrawingSession canvasDrawingSession;

        public DrawingSessionAdapter(CanvasDrawingSession canvasDrawingSession)
        {
            this.canvasDrawingSession = canvasDrawingSession ?? throw new ArgumentNullException(nameof(canvasDrawingSession));
        }

        public void Clear(Color color)
        {
            this.canvasDrawingSession.Clear(color.ToColor());
        }

        public void Close()
        {
            this.Dispose();
        }

        public void Dispose()
        {
            this.canvasDrawingSession.Dispose();
            this.canvasDrawingSession = null;
        }

        public void Draw(ITexture texture, Matrix3x2 transform, Vector4 tint)
        {
            CanvasBlend previousBlend = this.canvasDrawingSession.Blend;

            using (CanvasSpriteBatch spriteBatch = this.canvasDrawingSession.CreateSpriteBatch())
            {
                this.canvasDrawingSession.Blend = CanvasBlend.Add;

                CanvasBitmap bitmap = ((Texture)texture).Bitmap;
                spriteBatch.Draw(bitmap, transform, tint, CanvasSpriteFlip.None);

                this.canvasDrawingSession.Blend = previousBlend;
            }
        }

        public void Draw(ITexture texture, ISpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, Vector2.One, Vector4.One, 0.0f, Vector2.One, 1.0f);
        }

        public void DrawText(string text, Vector2 position, Color color)
        {
            this.canvasDrawingSession.DrawText(text, position, color.ToColor());
        }
    }
}
