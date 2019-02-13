// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Diagnostics;

namespace GameFramework.Core
{
    public class GameTimer
    {
        private static readonly long TicksPerSecond = 10000000;

        private readonly Stopwatch timer;

        private readonly long frequency;
        private readonly long maxDelta;

        private long lastTime;
        private long leftOverTicks;
        private long elapsedTicks;
        private long totalTicks;
        private long secondCounter;
        private long targetElapsedTicks;

        private int frameCount;
        private int framesPerSecond;
        private int framesThisSecond;

        private bool isFixedTimeStep;

        public GameTimer()
        {
            this.timer = new Stopwatch();
            this.frequency = Stopwatch.Frequency;
            this.lastTime = Stopwatch.GetTimestamp();

            this.maxDelta = this.frequency / 10;
            this.targetElapsedTicks = 0;
            this.isFixedTimeStep = true;
        }

        public long ElapsedTicks
        {
            get => this.elapsedTicks;
            set => this.elapsedTicks = value;
        }

        public long TotalTicks => this.totalTicks;

        public int FrameCount
        {
            get => this.frameCount;
            set => this.frameCount = value;
        }

        public int FramesPerSecond
        {
            get => this.framesPerSecond;
            set => this.framesPerSecond = value;
        }

        public bool IsFixedTimeStep
        {
            get => this.isFixedTimeStep;
            set => this.isFixedTimeStep = value;
        }

        public long TargetElapsedTicks
        {
            get => this.targetElapsedTicks;
            set => this.targetElapsedTicks = value;
        }

        public double TargetElapsedSeconds
        {
            set => this.targetElapsedTicks = (long)value * TicksPerSecond;
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
            if (this.isFixedTimeStep)
            {
                if (Math.Abs(timeDelta - this.targetElapsedTicks) < TicksPerSecond / 4000)
                {
                    timeDelta = this.targetElapsedTicks;
                }

                this.leftOverTicks += timeDelta;

                // Check if running slowly
                bool isRunningSlowly = this.leftOverTicks >= this.targetElapsedTicks * 2;

                while (this.leftOverTicks >= this.targetElapsedTicks)
                {
                    forceUpdate = false;
                    this.elapsedTicks = this.targetElapsedTicks;
                    this.totalTicks += this.targetElapsedTicks;
                    this.leftOverTicks -= this.targetElapsedTicks;
                    this.frameCount++;

                    action(isRunningSlowly);
                }

                if (forceUpdate)
                {
                    this.frameCount++;
                    action(false);
                }
            }
            else
            {
                this.elapsedTicks = timeDelta;
                this.totalTicks += timeDelta;
                this.leftOverTicks = 0;
                this.frameCount++;

                action(false);
            }

            if (this.frameCount != lastFrameCount)
            {
                this.framesThisSecond++;
            }

            if (this.secondCounter >= this.frequency)
            {
                this.framesPerSecond = this.framesThisSecond;
                this.framesThisSecond = 0;
                this.secondCounter %= this.frequency;
            }
        }

        public void Reset()
        {
            this.timer.Reset();
            this.timer.Start();
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
