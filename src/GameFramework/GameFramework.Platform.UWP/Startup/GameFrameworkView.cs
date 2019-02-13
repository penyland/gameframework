// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Platform;
using GameFramework.Platform.Utils;
using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace GameFramework
{
    public class GameFrameworkView<T> : IFrameworkView
        where T : IGameFactory, new()
    {
        private CoreWindowAdapter coreWindowAdapter;
        private GamePlatform<T> gamePlatform;

        public void Initialize(CoreApplicationView applicationView)
        {
            Debug.WriteLine("Initialize");
            applicationView.Activated += this.ApplicationView_Activated;
            CoreApplication.Suspending += this.CoreApplication_Suspending;
        }

        public void SetWindow(CoreWindow window)
        {
            Debug.WriteLine("SetWindow");
            TitleBarManager.ExtendViewIntoTitleBar(true);

            this.coreWindowAdapter = new CoreWindowAdapter(window);
        }

        public void Load(string entryPoint)
        {
            Debug.WriteLine("Load");
            this.gamePlatform = GamePlatform<T>.Create(this.coreWindowAdapter);
        }

        public void Run()
        {
            Debug.WriteLine("Run");
            this.gamePlatform.Run();
        }

        public void Uninitialize()
        {
            throw new System.NotImplementedException();
        }

        private void ApplicationView_Activated(CoreApplicationView sender, Windows.ApplicationModel.Activation.IActivatedEventArgs args)
        {
            Debug.WriteLine("ApplicationView_Activated");
            CoreWindow window = CoreWindow.GetForCurrentThread();
            var window2 = sender.CoreWindow;
            sender.CoreWindow.Activate();
        }

        private void CoreApplication_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            // game.Dispose();
            deferral.Complete();
        }
    }
}
