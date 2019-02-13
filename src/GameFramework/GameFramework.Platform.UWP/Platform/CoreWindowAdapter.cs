// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Microsoft.Graphics.Canvas;
using Windows.UI;
using Windows.UI.Core;

namespace GameFramework.Platform
{
    public class CoreWindowAdapter : IPlatformWindow
    {
        private readonly CoreWindow window;
        private readonly CanvasSwapChainAdapter canvasSwapChainAdapter;
        private CanvasDevice canvasDevice;
        private IGraphicsDevice graphicsDevice;

        public CoreWindowAdapter(CoreWindow window)
        {
            this.window = window;
            this.canvasDevice = new CanvasDevice();
            this.canvasSwapChainAdapter = new CanvasSwapChainAdapter(this.window, this.canvasDevice);

            this.window.SizeChanged += this.CoreWindow_SizeChanged;

            // TODO
            this.graphicsDevice = new CanvasDeviceAdapter(new CanvasDevice());
        }

        public IDrawingSession DrawingSession
        {
            get
            {
                this.canvasSwapChainAdapter.EnsureMatchesWindow(this.Window);

                CanvasSwapChain swapChain = this.canvasSwapChainAdapter.SwapChain;

                using (var ds = swapChain.CreateDrawingSession(Colors.Black))
                {
                    return new DrawingSession(ds);
                }
            }
        }

        public CoreWindow Window => this.window;

        public void OnActivated()
        {
        }

        /// <summary>
        /// Process window events.
        /// </summary>
        public void ProcessEvents()
        {
            CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);
        }

        public IDrawingSession CreateDrawingSession()
        {
            this.canvasSwapChainAdapter.EnsureMatchesWindow(this.Window);

            CanvasSwapChain swapChain = this.canvasSwapChainAdapter.SwapChain;

            return new DrawingSession(swapChain.CreateDrawingSession(Colors.Black));
        }

        public void Draw()
        {
            // if (not created)
            // this.canvasSwapChainAdapter.EnsureMatchesWindow(this.window);

            CanvasSwapChain swapChain = this.canvasSwapChainAdapter.SwapChain;

            // Hand over to Game.
            // using (CanvasDrawingSession ds = swapChain.CreateDrawingSession(Colors.Black))
            // {
            //    var drawingSession = new DrawingSession(ds);
            // }

            swapChain.Present();

            // If !fixed time steps
            // swapChain.WaitForVerticalBlank();
        }

        private void CoreWindow_SizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}
