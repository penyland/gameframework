// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Core;
using System;
using System.Numerics;

namespace GameFramework.Contracts
{
    public interface IPlatformWindow
    {
        event EventHandler<SizeChangedEventArgs> SizeChanged;

        event EventHandler Activated;

        event EventHandler Closed;

        event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;

        event EventHandler<float> DpiChanged;

        event EventHandler<int> OrientationChanged;

        Vector2 Size { get; }

        void ProcessEvents();
    }
}
