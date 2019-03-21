// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Core;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GameFramework
{
    public class GameWindow : IGameWindow
    {
        private readonly IPlatformWindow platformWindowAdapter;
        private readonly IGame game;
        private readonly GameTimer gameTimer = new GameTimer();

        private bool isVisible = true;
        private bool renderNeeded;
        private bool hasUpdated = false;

        private GameFrameworkState frameworkState = GameFrameworkState.WaitingForResources;

        public GameWindow(IPlatformWindow platformWindow, IGraphicsDevice graphicsDevice, IGame game)
        {
            this.GraphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.game = game ?? throw new ArgumentNullException(nameof(game));

            this.platformWindowAdapter = platformWindow ?? throw new ArgumentNullException(nameof(platformWindow));
            this.platformWindowAdapter.SizeChanged += this.OnSizeChanged;
            this.platformWindowAdapter.Activated += this.OnActivated;
            this.platformWindowAdapter.VisibilityChanged += this.OnVisibilityChanged;
            this.platformWindowAdapter.OrientationChanged += this.OnOrientationChanged;
            this.platformWindowAdapter.DpiChanged += this.OnDpiChanged;

            this.gameTimer.IsFixedTimeStep = false;
        }

        public IGraphicsDevice GraphicsDevice { get; }

        public async Task InitializeAsync()
        {
            if (this.frameworkState != GameFrameworkState.WaitingForResources)
            {
                return;
            }

            // Initialize framework state
            // Initialize game
            await Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("GameWindow.Initialize -> Calling game.Initialize");
                this.game?.Initialize();
                Debug.WriteLine("GameWindow.Initialize -> game.Initialize DONE");
            })
            .ContinueWith(async (_) =>
            {
                Debug.WriteLine("GameWindow.Initialize -> Calling game.CreateResourcesAsync");
                await this.game?.CreateResourcesAsync();
                this.frameworkState = GameFrameworkState.ResourcesLoaded;
                Debug.WriteLine("GameWindow.Initialize -> Calling game.CreateResourcesAsync - DONE");
            })
            .ContinueWith((_) =>
            {
                Debug.WriteLine("GameWindow.Initialize -> Transitioning to Paused");

                // Finalize framework state
                this.frameworkState = GameFrameworkState.Paused;

                // Start game loop
                Debug.WriteLine("GameWindow.Initialize -> Transitioning to Running");
                this.frameworkState = GameFrameworkState.Running;
            });
        }

        public async void Run()
        {
            Debug.WriteLine("GameWindow.Run()");
            Debug.WriteLine("GameWindow.Run() - Starting gameloop");

            while (true)
            {
                if (this.isVisible)
                {
                    switch (this.frameworkState)
                    {
                        case GameFrameworkState.WaitingForResources:
                            this.InitializeAsync();
                            break;
                        case GameFrameworkState.ResourcesLoaded:
                            break;
                        case GameFrameworkState.Suspended:
                            break;
                        case GameFrameworkState.Deactivated:
                            break;
                        case GameFrameworkState.Paused:
                            break;
                        case GameFrameworkState.Running:
                            {
                                this.platformWindowAdapter.ProcessEvents();

                                this.Tick();
                            }

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    this.platformWindowAdapter.ProcessEvents();
                }
            }
        }

        public void Tick()
        {
            // Update
            bool forceUpdate = false;

            if (!this.hasUpdated)
            {
                this.gameTimer.ResetElapsedTime();
                forceUpdate = true;
            }

            this.hasUpdated = this.Update(forceUpdate, 0);

            // Only Draw/Present if an Update has actually happened.
            bool drew = false;
            if (this.hasUpdated)
            {
                if (this.GraphicsDevice.SwapChain != null)
                {
                    // Create drawing session
                    using (IDrawingSession drawingSession = this.GraphicsDevice.CreateDrawingSession())
                    {
                        GameTime gameTime = this.CreateTimingInfo(false);
                        this.game.Draw(gameTime, drawingSession);
                    }

                    this.GraphicsDevice.Present();

                    drew = true;
                }
            }

            if (!drew || !this.gameTimer.IsFixedTimeStep)
            {
                if (this.GraphicsDevice.SwapChain != null)
                {
                    // WaitForVerticalBlank
                    this.GraphicsDevice.SwapChain.WaitForVerticalBlank();
                }
                else
                {
                    Task.Delay((int)this.gameTimer.DefaultTargetElapsedTime * 1000);
                }
            }
        }

        private bool Update(bool forceUpdate, long timeSpentPaused)
        {
            this.gameTimer.Tick(
                forceUpdate,
                timeSpentPaused,
                (isRunningSlowly) =>
                {
                    GameTime timingInfo = this.CreateTimingInfo(isRunningSlowly);

                    this.game.Update(timingInfo);
                });

            return true;
        }

        private void OnActivated(object sender, EventArgs e)
        {
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.GraphicsDevice.Size = e.Size;
        }

        private void OnVisibilityChanged(object sender, VisibilityChangedEventArgs e)
        {
            if (e.Visible && !this.isVisible)
            {
                this.renderNeeded = true;
            }

            this.isVisible = e.Visible;
        }

        private void OnOrientationChanged(object sender, int e)
        {
        }

        private void OnDpiChanged(object sender, float e)
        {
            this.GraphicsDevice.LogicalDpi = e;
        }

        private GameTime CreateTimingInfo(bool isRunningSlowly)
        {
            return new GameTime()
            {
                UpdateCount = this.gameTimer.FrameCount,
                ElapsedGameTime = TimeSpan.FromTicks(this.gameTimer.ElapsedTicks),
                TotalGameTime = TimeSpan.FromTicks(this.gameTimer.TotalTicks),
                IsRunningSlowly = isRunningSlowly,
            };
        }
    }
}
