// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;

namespace GameFramework.Abstractions
{
    public class GameTime
    {
        public GameTime()
        {
            this.TotalGameTime = TimeSpan.Zero;
            this.ElapsedGameTime = TimeSpan.Zero;
            this.IsRunningSlowly = false;
        }

        public GameTime(TimeSpan totalGameTime, TimeSpan elapsedGameTime)
        {
            this.TotalGameTime = totalGameTime;
            this.ElapsedGameTime = elapsedGameTime;
            this.IsRunningSlowly = false;
        }

        public GameTime(TimeSpan totalRealTime, TimeSpan elapsedRealTime, bool isRunningSlowly)
        {
            this.TotalGameTime = totalRealTime;
            this.ElapsedGameTime = elapsedRealTime;
            this.IsRunningSlowly = isRunningSlowly;
        }

        public TimeSpan TotalGameTime { get; set; }

        public TimeSpan ElapsedGameTime { get; set; }

        public bool IsRunningSlowly { get; set; }

        public long UpdateCount { get; set; }
    }
}
