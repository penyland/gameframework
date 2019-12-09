// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Platform.Extensions;
using System;
using System.Numerics;
using Windows.Graphics.Display;
using Windows.UI.Core;

namespace GameFramework.Platform
{
    public sealed class CoreWindowAdapter : IPlatformWindow
    {
        private readonly ICoreApplicationContext coreApplicationContext;

        public CoreWindowAdapter(ICoreApplicationContext coreApplicationContext)
        {
            this.coreApplicationContext = coreApplicationContext ?? throw new ArgumentNullException(nameof(coreApplicationContext));
        }

        public event EventHandler<SizeChangedEventArgs> SizeChanged;

        public event EventHandler Activated;

        public event EventHandler Closed;

        public event EventHandler<GameFramework.Abstractions.VisibilityChangedEventArgs> VisibilityChanged;

        public event EventHandler<float> DpiChanged;

        public event EventHandler<int> OrientationChanged;

        public object Window { get; internal set; }

        public Vector2 Size { get; internal set; }

        private CoreWindow CoreWindow { get; set; }

        public void Initialize()
        {
            this.Window = this.coreApplicationContext.CoreWindow;
            this.CoreWindow = this.coreApplicationContext.CoreWindow;

            this.Size = this.CoreWindow.Bounds.ToVector2();

            this.CoreWindow.Activated += this.Window_Activated;
            this.CoreWindow.SizeChanged += this.CoreWindow_SizeChanged;
            this.CoreWindow.VisibilityChanged += this.Window_VisibilityChanged;
            this.CoreWindow.Closed += this.Window_Closed;

            var currentDisplayInformation = DisplayInformation.GetForCurrentView();
            currentDisplayInformation.DpiChanged += this.OnDpiChanged;
            currentDisplayInformation.OrientationChanged += this.OnOrientationChanged;
        }

        /// <summary>
        /// Process window events.
        /// </summary>
        public void ProcessEvents(bool isVisible = true)
        {
            if (isVisible)
            {
                CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);
            }
            else
            {
                CoreWindow.GetForCurrentThread().Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessOneAndAllPending);
            }
        }

        private void Window_Activated(CoreWindow sender, WindowActivatedEventArgs args)
        {
            this.Activated?.Invoke(sender, null);
        }

        private void Window_VisibilityChanged(CoreWindow sender, Windows.UI.Core.VisibilityChangedEventArgs args)
        {
            this.VisibilityChanged?.Invoke(this, new GameFramework.Abstractions.VisibilityChangedEventArgs(args.Visible));
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
