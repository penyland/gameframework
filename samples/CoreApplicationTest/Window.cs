// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Platform;
using System;
using System.Numerics;
using Windows.UI.Core;

namespace CoreApplicationTest
{
    public class Window : IGameWindow
    {
        private readonly IGraphicsDevice graphicsDevice;
        private readonly IPlatformWindow platformWindow;
        private bool isVisible = true;
        private bool isClosed = false;

        public Window(IGraphicsDevice graphicsDevice, IPlatformWindow platformWindow)
        {
            this.graphicsDevice = graphicsDevice ?? throw new ArgumentNullException(nameof(graphicsDevice));
            this.platformWindow = platformWindow ?? throw new ArgumentNullException(nameof(platformWindow));
        }

        public int Width { get; set; }

        public int Height { get; set; }

        public void Initialize()
        {
            this.platformWindow.Initialize();
            this.graphicsDevice.Initialize();

            this.Width = (int)this.platformWindow.Size.X;
            this.Height = (int)this.platformWindow.Size.Y;

            this.platformWindow.SizeChanged += this.PlatformWindow_SizeChanged;
            this.platformWindow.DpiChanged += this.PlatformWindow_DpiChanged;
            this.platformWindow.VisibilityChanged += this.PlatformWindow_VisibilityChanged;
            this.platformWindow.Closed += this.PlatformWindow_Closed;
        }

        public void Run()
        {
            while (!this.isClosed)
            {
                if (this.isVisible)
                {
                    this.platformWindow.ProcessEvents();

                    using (IDrawingSession ds = this.graphicsDevice.CreateDrawingSession())
                    {
                        ds.Clear(Colors.Black);
                        ds.DrawText("Hello GameFramework", new Vector2(0, 0), Colors.White);
                    }

                    this.graphicsDevice.Present();
                }
                else
                {
                    this.platformWindow.ProcessEvents();
                }
            }
        }

        private void PlatformWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.Width = (int)e.Size.X;
            this.Height = (int)e.Size.Y;

            this.graphicsDevice.Size = e.Size;
        }

        private void PlatformWindow_DpiChanged(object sender, float e)
        {
            this.graphicsDevice.LogicalDpi = e;
        }

        private void PlatformWindow_Closed(object sender, EventArgs e)
        {
            this.isClosed = true;
        }

        private void PlatformWindow_VisibilityChanged(object sender, GameFramework.Abstractions.VisibilityChangedEventArgs e)
        {
            this.isVisible = e.Visible;
        }
    }
}
