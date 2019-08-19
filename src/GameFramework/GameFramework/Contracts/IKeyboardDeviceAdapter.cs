// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Input;

namespace GameFramework.Contracts
{
    public interface IKeyboardDeviceAdapter
    {
        bool IsConnected { get; }
    }
}
