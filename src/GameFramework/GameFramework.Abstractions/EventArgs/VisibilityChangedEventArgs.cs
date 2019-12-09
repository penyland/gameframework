// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace GameFramework.Abstractions
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
