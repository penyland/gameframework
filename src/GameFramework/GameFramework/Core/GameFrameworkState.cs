using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Core
{
    public enum GameFrameworkState
    {
        WaitingForResources,
        ResourcesLoaded,
        Suspended,
        Deactivated,
        Paused,
        Running,
    }
}
