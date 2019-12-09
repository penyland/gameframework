// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using Microsoft.Graphics.Canvas;
using System.Numerics;

namespace GameFramework.Platform.Graphics
{
    public class Texture : ITexture
    {
        public Texture(CanvasBitmap canvasBitmap)
        {
            this.Bitmap = canvasBitmap;
        }

        public Vector2 Center => this.Bitmap.Size.ToVector2() / 2;

        public Vector2 Size => this.Bitmap.Size.ToVector2();

        public CanvasBitmap Bitmap { get; set; }
    }
}
