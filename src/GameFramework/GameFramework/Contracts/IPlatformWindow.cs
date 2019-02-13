// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Contracts
{
    public interface IPlatformWindow
    {
        IDrawingSession DrawingSession { get; }

        void OnActivated();

        void ProcessEvents();

        void Draw();

        IDrawingSession CreateDrawingSession();
    }
}
