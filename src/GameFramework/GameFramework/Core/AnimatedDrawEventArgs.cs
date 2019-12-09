// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;

namespace GameFramework.Core
{
    public class AnimatedDrawEventArgs
    {
        public AnimatedDrawEventArgs(IDrawingSession drawingSession, GameTime gameTime)
        {
            this.DrawingSession = drawingSession;
            this.TimingInfo = gameTime;
        }

        // DrawingSession
        public IDrawingSession DrawingSession { get; }

        // Timing
        public GameTime TimingInfo { get; }
    }
}
