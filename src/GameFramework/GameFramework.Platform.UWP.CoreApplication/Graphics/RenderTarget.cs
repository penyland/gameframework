// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using Microsoft.Graphics.Canvas;

namespace GameFramework.Platform.Graphics
{
    public class RenderTarget : Texture2D, IRenderTarget
    {
        public RenderTarget(CanvasRenderTarget canvasRenderTarget)
            : base(canvasRenderTarget)
        {
            this.CanvasRenderTarget = canvasRenderTarget;
        }

        public CanvasRenderTarget CanvasRenderTarget { get; }

        public IDrawingSession CreateDrawingSession()
        {
            return new DrawingSessionAdapter(this.CanvasRenderTarget.CreateDrawingSession());
        }
    }
}
