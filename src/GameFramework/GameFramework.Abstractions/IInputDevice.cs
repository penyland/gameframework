// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace GameFramework.Abstractions
{
    public interface IInputDevice
    {
        bool IsConnected { get; }

        void Initialize();

        void Reset();

        void Update();
    }
}
