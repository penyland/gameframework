// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Numerics;

namespace GameFramework.Abstractions
{
    public class SizeChangedEventArgs : EventArgs
    {
        public SizeChangedEventArgs(Vector2 size)
        {
            this.Size = size;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window size event was handled.
        /// </summary>
        /// <remarks>
        ///     True if the window size event has been handled by the appropriate delegate; false
        ///     if it has not.
        /// </remarks>
        public bool Handled { get; set; }

        /// <summary>
        /// Gets the new size of the window in units of effective (view) pixels.
        /// </summary>
        public Vector2 Size { get; }
    }
}
