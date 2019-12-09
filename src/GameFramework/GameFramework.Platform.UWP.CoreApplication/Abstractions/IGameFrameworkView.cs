// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace GameFramework.Platform.Abstractions
{
    public interface IGameFrameworkView
    {
        event EventHandler OnActivated;

        void Activate();
    }
}
