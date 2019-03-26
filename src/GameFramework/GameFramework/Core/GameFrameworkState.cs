// Copyright (c) Peter Nylander.  All rights reserved.

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
