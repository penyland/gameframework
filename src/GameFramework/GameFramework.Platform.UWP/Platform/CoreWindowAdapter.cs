// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Core;
using GameFramework.Extensions;
using System;
using System.Numerics;
using Windows.Graphics.Display;
using Windows.UI.Core;

namespace GameFramework.Platform
{
    public sealed class CoreWindowAdapter : IPlatformWindow
    {
        private readonly DisplayInformation currentDisplayInformation;

        private CoreWindowAdapter(CoreWindow window)
        {
            this.Window = window;
            this.Size = this.Window.Bounds.ToVector2();

            this.Window.Activated += this.Window_Activated;
            this.Window.SizeChanged += this.CoreWindow_SizeChanged;
            this.Window.VisibilityChanged += this.Window_VisibilityChanged;
            this.Window.Closed += this.Window_Closed;

            this.currentDisplayInformation = DisplayInformation.GetForCurrentView();
            this.currentDisplayInformation.DpiChanged += this.OnDpiChanged;
            this.currentDisplayInformation.OrientationChanged += this.OnOrientationChanged;
        }

        public event EventHandler<SizeChangedEventArgs> SizeChanged;

        public event EventHandler Activated;

        public event EventHandler Closed;

        public event EventHandler<Core.VisibilityChangedEventArgs> VisibilityChanged;

        public event EventHandler<float> DpiChanged;

        public event EventHandler<int> OrientationChanged;

        public CoreWindow Window { get; }

        public Vector2 Size { get; internal set; }

        public static IPlatformWindow Create(CoreWindow window)
        {
            return new CoreWindowAdapter(window);
        }

        /// <summary>
        /// Process window events.
        /// </summary>
        public void ProcessEvents()
        {
            CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);
        }

        private void Window_Activated(CoreWindow sender, WindowActivatedEventArgs args)
        {
            this.Activated?.Invoke(sender, null);
        }

        private void Window_VisibilityChanged(CoreWindow sender, Windows.UI.Core.VisibilityChangedEventArgs args)
        {
            this.VisibilityChanged?.Invoke(this, new Core.VisibilityChangedEventArgs(args.Visible));
        }

        private void Window_Closed(CoreWindow sender, CoreWindowEventArgs args)
        {
            this.Closed?.Invoke(this, EventArgs.Empty);
        }

        private void CoreWindow_SizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            this.Size = args.Size.ToVector2();
            this.SizeChanged?.Invoke(this, new SizeChangedEventArgs(args.Size.ToVector2()));
        }

        private void OnOrientationChanged(DisplayInformation sender, object args)
        {
            this.OrientationChanged?.Invoke(this, (int)sender.NativeOrientation);
        }

        private void OnDpiChanged(DisplayInformation sender, object args)
        {
            this.DpiChanged?.Invoke(this, sender.LogicalDpi);
        }
    }
}
