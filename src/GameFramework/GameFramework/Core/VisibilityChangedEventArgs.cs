// Copyright (c) Peter Nylander.  All rights reserved.

using System;

namespace GameFramework.Core
{
    public class VisibilityChangedEventArgs : EventArgs
    {
        public VisibilityChangedEventArgs(bool visible)
        {
            this.Visible = visible;
        }

        public bool Visible { get; }
    }
}
