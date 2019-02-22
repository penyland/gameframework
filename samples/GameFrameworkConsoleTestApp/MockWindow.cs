// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Core;
using System;
using System.Numerics;

namespace GameFrameworkConsoleTestApp
{
    internal class MockWindow : IPlatformWindow
    {
        public MockWindow()
        {
        }

        public event EventHandler<SizeChangedEventArgs> SizeChanged;

        public event EventHandler Activated;

        public event EventHandler Closed;

        public event EventHandler<VisibilityChangedEventArgs> VisibilityChanged;

        public event EventHandler<float> DpiChanged;

        public event EventHandler<int> OrientationChanged;

        public IDrawingSession DrawingSession { get; }

        public Vector2 Size { get; }

        public void OnActivated()
        {
            throw new NotImplementedException();
        }

        public void ProcessEvents()
        {
            //Console.Read();
        }
    }
}
