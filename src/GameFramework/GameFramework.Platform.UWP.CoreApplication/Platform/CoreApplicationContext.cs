// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Windows.UI.Core;

namespace GameFramework.Platform
{
    public class CoreApplicationContext : ICoreApplicationContext
    {
        public CoreApplicationContext()
        {
        }

        public bool IsRunning { get; set; }

        public CoreWindow CoreWindow { get; set; }

        public void Start()
        {
        }
    }
}
