// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using Windows.UI.Core;

namespace GameFramework.Platform
{
    public interface ICoreApplicationContext : IGameFrameworkContext
    {
        CoreWindow CoreWindow { get; set; }
    }
}
