// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Core;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GameFramework
{
    /// <summary>
    /// TODO: Rename to GameLoop and remove/move all window event handling.
    /// </summary>
    public class GameWindow : IGameWindow
    {
        private readonly IPlatformWindow platformWindow;
        private readonly IGame game;
        private readonly GameTimer gameTimer = new GameTimer();

        private bool isVisible = true;
        private bool renderNeeded;
        private bool hasUpdated = false;

        private GameFrameworkState frameworkState;
        private object stateLock = new object();

        public GameWindow(IPlatformWindow platformWindow, IGraphicsDevice graphicsDevice, IGame game)
        {
            this.GraphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.game = game ?? throw new ArgumentNullException(nameof(game));

            this.platformWindow = platformWindow ?? throw new ArgumentNullException(nameof(platformWindow));
            this.platformWindow.SizeChanged += this.OnSizeChanged;
            this.platformWindow.Activated += this.OnActivated;
            this.platformWindow.VisibilityChanged += this.OnVisibilityChanged;
            this.platformWindow.OrientationChanged += this.OnOrientationChanged;
            this.platformWindow.DpiChanged += this.OnDpiChanged;

            this.gameTimer.IsFixedTimeStep = false;
        }

        public IGraphicsDevice GraphicsDevice { get; }

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
            Debug.WriteLine("GameWindow.Initialize -> Calling game.Initialize");
            this.State = GameFrameworkState.Initializing;
            this.game?.Initialize();
            Debug.WriteLine("GameWindow.Initialize -> game.Initialize DONE");

            // Load resources asynchronously
            await Task.Run(async () =>
            {
                Debug.WriteLine("GameWindow.Initialize -> Calling game.CreateResourcesAsync");
                await this.game?.CreateResourcesAsync();
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
                    this.platformWindow.ProcessEvents();
                }
                else
                {
                    this.platformWindow.ProcessEvents(false);
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
                    this.game.BeginDraw();

                    this.game.Draw(this.CreateTimingInfo(false));

                    this.GraphicsDevice.Present();

                    this.game.EndDraw();

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

        public void Initialize()
        {
            this.platformWindow.Initialize();
            this.GraphicsDevice.Initialize();
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

        private void Draw(GameTime gameTime, IDrawingSession drawingSession)
        {
            this.game.Draw(gameTime, drawingSession);
        }

        private void OnActivated(object sender, EventArgs e)
        {
        }

        private void OnSizeChanged(object sender, Abstractions.SizeChangedEventArgs e)
        {
            this.GraphicsDevice.Size = e.Size;
        }

        private void OnVisibilityChanged(object sender, Abstractions.VisibilityChangedEventArgs e)
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
