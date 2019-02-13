// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;

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
