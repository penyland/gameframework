// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace GameFramework.Abstractions
{
    public interface IGraphicsDeviceAdapter
    {
        event EventHandler DeviceLost;

        void CreateDeviceResources();

        void Dispose();

        void Trim();
    }
}
