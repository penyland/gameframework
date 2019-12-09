// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Diagnostics;

namespace GameFramework.Core
{
    public class GameTimer
    {
        private const long TicksPerSecond = TimeSpan.TicksPerSecond;

        private readonly long frequency;
        private readonly long maxDelta;

        private long lastTime;
        private long leftOverTicks;
        private long secondCounter;
        private int framesThisSecond;

        public GameTimer()
        {
            this.frequency = Stopwatch.Frequency;
            this.lastTime = Stopwatch.GetTimestamp();

            this.maxDelta = this.frequency / 10;
            this.TargetElapsedTicks = 0;
            this.IsFixedTimeStep = true;
            this.TargetElapsedTicks = this.DefaultTargetElapsedTime;
        }

        public long DefaultTargetElapsedTime => TicksPerSecond / 60;

        public long ElapsedTicks { get; internal set; }

        public long TotalTicks { get; internal set; }

        public int FrameCount { get; internal set; }

        public int FramesPerSecond { get; internal set; }

        public bool IsFixedTimeStep { get; internal set; }

        public long TargetElapsedTicks { get; internal set; }

        public double TargetElapsedSeconds
        {
            set => this.TargetElapsedTicks = (long)value * TicksPerSecond;
        }

        public void Tick(bool forceUpdate, long timeSpentPaused, Action<bool> action)
        {
            long currentTime = Stopwatch.GetTimestamp();
            long timeDelta = currentTime - this.lastTime;

            // Compensate for the time that was spent paused
            timeDelta -= Math.Min(timeDelta, Math.Max(0, timeSpentPaused));

            this.lastTime = currentTime;
            this.secondCounter += timeDelta;

            if (timeDelta > this.maxDelta)
            {
                timeDelta = this.maxDelta;
            }

            timeDelta *= TicksPerSecond;
            timeDelta /= this.frequency;

            int lastFrameCount = this.FrameCount;
            if (this.IsFixedTimeStep)
            {
                if (Math.Abs(timeDelta - this.TargetElapsedTicks) < TicksPerSecond / 4000)
                {
                    timeDelta = this.TargetElapsedTicks;
                }

                this.leftOverTicks += timeDelta;

                // Check if running slowly
                bool isRunningSlowly = this.leftOverTicks >= this.TargetElapsedTicks * 2;

                while (this.leftOverTicks >= this.TargetElapsedTicks)
                {
                    forceUpdate = false;
                    this.ElapsedTicks = this.TargetElapsedTicks;
                    this.TotalTicks += this.TargetElapsedTicks;
                    this.leftOverTicks -= this.TargetElapsedTicks;
                    this.FrameCount++;

                    action(isRunningSlowly);
                }

                if (forceUpdate)
                {
                    this.FrameCount++;
                    action(false);
                }
            }
            else
            {
                this.ElapsedTicks = timeDelta;
                this.TotalTicks += timeDelta;
                this.leftOverTicks = 0;
                this.FrameCount++;

                action(false);
            }

            if (this.FrameCount != lastFrameCount)
            {
                this.framesThisSecond++;
            }

            if (this.secondCounter >= this.frequency)
            {
                this.FramesPerSecond = this.framesThisSecond;
                this.framesThisSecond = 0;
                this.secondCounter %= this.frequency;
            }
        }

        public void ResetElapsedTime()
        {
            this.lastTime = Stopwatch.GetTimestamp();

            this.leftOverTicks = 0;
            this.FramesPerSecond = 0;
            this.framesThisSecond = 0;
            this.secondCounter = 0;
        }
    }
}
