// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;

namespace GameFramework.Abstractions
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

        object Window { get; }

        void ProcessEvents(bool isVisible = true);

        void Initialize();
    }
}
