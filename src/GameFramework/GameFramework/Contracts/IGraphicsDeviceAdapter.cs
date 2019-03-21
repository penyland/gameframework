// Copyright (c) Peter Nylander.  All rights reserved.

using System;

namespace GameFramework.Contracts
{
    public interface IGraphicsDeviceAdapter
    {
        event EventHandler DeviceLost;

        void CreateDeviceResources();

        void Dispose();

        void Trim();
    }
}
