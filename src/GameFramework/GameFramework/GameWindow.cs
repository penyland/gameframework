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

        private GameFrameworkState frameworkState;
        private object stateLock = new object();

        public GameWindow(IPlatformWindow platformWindow, IGraphicsDevice graphicsDevice, IInputManager inputManager, IGame game)
        {
            this.GraphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.InputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
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

        public IInputManager InputManager { get; }

        private GameFrameworkState State
        {
            get
            {
                lock (this.stateLock)
                {
                    return this.frameworkState;
                }
            }

            set
            {
                lock (this.stateLock)
                {
                    this.frameworkState = value;
                }
            }
        }

        public async void InitializeAsync()
        {
            if (this.State != GameFrameworkState.NotInitialized)
            {
                return;
            }

            // Initialize framework state
            // Initialize game
            await Task.Factory.StartNew(() =>
            {
                Debug.WriteLine("GameWindow.Initialize -> Calling game.Initialize");
                this.State = GameFrameworkState.Initializing;
                this.game?.Initialize();
                Debug.WriteLine("GameWindow.Initialize -> game.Initialize DONE");
            })
            .ContinueWith((_) =>
            {
                Debug.WriteLine("GameWindow.Initialize -> Calling game.CreateResourcesAsync");
                this.game?.CreateResourcesAsync();
                this.State = GameFrameworkState.ResourcesLoaded;
                Debug.WriteLine("GameWindow.Initialize -> Calling game.CreateResourcesAsync - DONE");
            })
            .ContinueWith((_) =>
            {
                Debug.WriteLine("GameWindow.Initialize -> Transitioning to Paused");

                // Finalize framework state
                this.State = GameFrameworkState.Paused;

                // Start game loop
                Debug.WriteLine("GameWindow.Initialize -> Transitioning to Running");
                this.State = GameFrameworkState.Running;
            });
        }

        public void Run()
        {
            Debug.WriteLine("GameWindow.Run()");
            Debug.WriteLine("GameWindow.Run() - Starting gameloop");

            while (true)
            {
                if (this.isVisible)
                {
                    switch (this.State)
                    {
                        case GameFrameworkState.NotInitialized:
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
                                this.Tick();
                            }

                            break;
                        default:
                            break;
                    }
                }
                else
                {
                }

                this.platformWindowAdapter.ProcessEvents();
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
            // Process window events
            this.InputManager.Update();

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
