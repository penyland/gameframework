// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Utils;
using Microsoft.Graphics.Canvas;
using System;
using System.Numerics;

namespace GameFramework.Platform
{
    public class CanvasSpriteBatchWrapper : ISpriteBatch, IDisposable
    {
        private readonly CanvasSpriteBatch canvasSpriteBatch;

        public CanvasSpriteBatchWrapper(CanvasSpriteBatch canvasSpriteBatch)
        {
            this.canvasSpriteBatch = canvasSpriteBatch;
        }

        public void Dispose()
        {
            this.canvasSpriteBatch.Dispose();
        }

        public void Draw(ITexture texture, Matrix3x2 transform, Vector4 tint)
        {
            CanvasBitmap bitmap = ((Texture)texture).Bitmap;
            this.canvasSpriteBatch.Draw(bitmap, transform, tint, CanvasSpriteFlip.None);
        }

        public void Draw(ITexture texture, Vector2 position, Vector4 tint, float rotation, Vector2 origin, float scale)
        {
            Matrix3x2 transformationMatrix =
                    MathUtils.CreateTransformMatrix(
                        rotation,
                        scale,
                        position,
                        texture.Center);

            this.Draw(texture, transformationMatrix, tint);
        }
    }
}
