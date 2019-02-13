// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using System;
using System.Drawing;
using System.Numerics;
using System.Threading.Tasks;

namespace GameFramework.Core
{
    public class AnimatedGameWindow : IGameLoopClient
    {
        private object gameLoop;
        private GameTimer gameTimer;
        private bool isLoaded = false;

        private ISwapChain swapChain;

        public AnimatedGameWindow(IPlatformWindow window, ISwapChain swapChain, IGraphicsDevice device)
        {
            this.Window = window;
            this.swapChain = swapChain;
            this.Device = device;
            this.gameTimer = new GameTimer();
        }

        /* Base */
        public event EventHandler CreateResources;

        public event EventHandler Update;

        public event EventHandler<AnimatedDrawEventArgs> Draw;

        public event EventHandler GameLoopStarting;

        public event EventHandler GameLoopStopped;

        public bool Paused { get; set; }

        public Size Size { get; }

        public Vector4 ClearColor { get; set; }

        public float LogicalDpi { get; }

        public bool IsFixedTimeStep { get; set; }

        public bool HasGameLoopThreadAccess { get; }

        public TimeSpan TargetElapsedTime { get; set; }

        public IPlatformWindow Window { get; }

        public IGraphicsDevice Device { get; }

        public void Create()
        {
        }

        public void Invalidate()
        {
        }

        public void ResetElapsedTime()
        {
        }

        public void Loaded()
        {
            //this.gameLoop = GetAdapter().CreateAndStartGameLoop(this, swapChain);
        }

        public void UnLoaded()
        {
        }

        public void Changed(ChangeReason changeReason)
        {
        }

        public Task RunOnGameLoopThreadAsync(Action action)
        {
            if (this.isLoaded)
            {
                // Add to PendingAsyncActionsQueue
            }
            else
            {
                // Async action.Cancel();
            }

            // Changed()

            return Task.FromResult<object>(null);
        }

        private AnimatedDrawEventArgs CreateDrawEventArgs(IDrawingSession drawingSession, bool isRunningSlowly)
        {
            return new AnimatedDrawEventArgs(drawingSession, new GameTime { IsRunningSlowly = isRunningSlowly });
        }

        private bool Tick()
        {
            this.gameTimer.IsFixedTimeStep = true;

            this.OnUpdate(false);

            // Are resources created
            if (this.swapChain != null)
            {
                this.OnDraw();

                this.swapChain.Present();
            }

            return false;
        }

        private void OnUpdate(bool forceUpdate)
        {
            this.gameTimer.Tick(forceUpdate, 0, (isRunningSlowly) =>
            {
                var gameTime = new GameTime
                {
                    ElapsedGameTime = TimeSpan.FromTicks(this.gameTimer.ElapsedTicks),
                    TotalGameTime = TimeSpan.FromTicks(this.gameTimer.TotalTicks),
                    IsRunningSlowly = isRunningSlowly,
                    UpdateCount = this.gameTimer.FrameCount,
                };

                // Create UpdateEventArgs
                this.Update?.Invoke(this, null);
            });
        }

        private void OnDraw()
        {
            IDrawingSession ds = this.swapChain.CreateDrawingSession();
            var eventArgs = new AnimatedDrawEventArgs(ds, null);
            this.Draw?.Invoke(this, eventArgs);

            ds.Close();
        }

        private ISwapChain CreateOrUpdateRenderTarget(
            IGraphicsDevice device,
            AlphaMode newAlphaMode,
            float newDpi,
            Size newSize,
            ISwapChain renderTarget)
        {
            bool needsTarget = renderTarget != null;
            bool alphaModeChanged = renderTarget.AlphaMode != newAlphaMode;
            bool dpiChanged = renderTarget.Dpi != newDpi;
            bool sizeChanged = renderTarget.Size != newSize;
            bool needsCreate = needsTarget || alphaModeChanged;

            if (!needsCreate && !sizeChanged && !dpiChanged)
            {
                return null;
            }

            if (newSize.Width <= 0 || newSize.Height <= 0)
            {
                // Zero-sized controls don't have swap chain objects
                return null;
            }
            else if ((sizeChanged || dpiChanged) && !needsCreate)
            {
                renderTarget.ResizeBuffersWithWidthAndHeightAndDpi(newSize.Width, newSize.Height, newDpi);
                renderTarget.Size = newSize;
                renderTarget.Dpi = newDpi;
            }
            else
            {
                renderTarget.Target = this.Device.CreateSwapChain(
                    newSize.Width,
                    newSize.Height,
                    newDpi,
                    newAlphaMode);

                renderTarget.AlphaMode = newAlphaMode;
                renderTarget.Dpi = newDpi;
                renderTarget.Size = newSize;
            }

            return renderTarget;
        }

        public void OnGameLoopStarting()
        {
            throw new NotImplementedException();
        }

        public void OnGameLoopStopped()
        {
            throw new NotImplementedException();
        }

        public bool Tick(ISwapChain swapChain, bool areResourcesCreated)
        {
            throw new NotImplementedException();
        }

        public void OnTickLoopEnded()
        {
            throw new NotImplementedException();
        }

        /*
        virtual void ApplicationSuspending(ISuspendingEventArgs* args) override final;
        virtual void ApplicationResuming() override final;
        virtual void WindowVisibilityChanged() override final;
         */
    }
}
