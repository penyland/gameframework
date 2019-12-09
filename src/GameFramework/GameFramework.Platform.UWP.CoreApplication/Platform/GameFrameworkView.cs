// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using GameFramework.Abstractions;
using GameFramework.Platform.Abstractions;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace GameFramework.Platform
{
    public class GameFrameworkView : IFrameworkViewSource, IFrameworkView, IGameFrameworkView
    {
        private readonly IGameWindow window;
        private readonly ICoreApplicationContext coreApplicationContext;
        private readonly IPlatformWindow platformWindow;
        private readonly IInputManager inputManager;

        public GameFrameworkView(
            IGameWindow gameWindow,
            ICoreApplicationContext coreApplicationContext,
            IPlatformWindow platformWindow,
            IInputManager inputManager)
        {
            this.window = gameWindow ?? throw new ArgumentNullException(nameof(gameWindow));
            this.coreApplicationContext = coreApplicationContext ?? throw new ArgumentNullException(nameof(coreApplicationContext));
            this.platformWindow = platformWindow ?? throw new ArgumentNullException(nameof(platformWindow));
            this.inputManager = inputManager ?? throw new ArgumentNullException(nameof(inputManager));
        }

        public event EventHandler OnActivated;

        public IFrameworkView CreateView()
        {
            return this;
        }

        public void Initialize(CoreApplicationView applicationView)
        {
            applicationView.Activated += this.ApplicationView_Activated;
            CoreApplication.Suspending += this.CoreApplication_Suspending;
            CoreApplication.Resuming += this.CoreApplication_Resuming;
        }

        public void SetWindow(CoreWindow coreWindow)
        {
            this.coreApplicationContext.CoreWindow = coreWindow;
            this.window.Initialize();
        }

        public void Load(string entryPoint)
        {
        }

        public void Run()
        {
            this.window.Run();
        }

        public void Uninitialize()
        {
        }

        public void Activate()
        {
            var window = CoreWindow.GetForCurrentThread();
            window.Activate();
        }

        private void ApplicationView_Activated(CoreApplicationView sender, Windows.ApplicationModel.Activation.IActivatedEventArgs args)
        {
            this.OnActivated?.Invoke(this, EventArgs.Empty);
        }

        private void CoreApplication_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }

        private void CoreApplication_Resuming(object sender, object e)
        {
        }
    }
}
