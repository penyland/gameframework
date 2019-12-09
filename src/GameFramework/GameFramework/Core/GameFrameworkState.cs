// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace GameFramework.Core
{
    public enum GameFrameworkState
    {
        NotInitialized,
        Initializing,
        WaitingForResources,
        ResourcesLoaded,
        Suspended,
        Deactivated,
        Paused,
        Running,
    }
}
