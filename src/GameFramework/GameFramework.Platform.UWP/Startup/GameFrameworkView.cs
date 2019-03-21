// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using GameFramework.Platform;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace GameFramework
{
    public class GameFrameworkView<T> : IFrameworkView
        where T : IGameFactory, new()
    {
        private GamePlatform<T> gamePlatform;

        public void Initialize(CoreApplicationView applicationView)
        {
            applicationView.Activated += this.ApplicationView_Activated;
            CoreApplication.Suspending += this.CoreApplication_Suspending;
            CoreApplication.Resuming += this.CoreApplication_Resuming;
        }

        public void SetWindow(CoreWindow window)
        {
            // TitleBarManager.ExtendViewIntoTitleBar(true);

            this.gamePlatform =
                GamePlatform<T>.Create(
                    CoreWindowAdapter.Create(window),
                    PlatformFactory.Create());
        }

        public void Load(string entryPoint)
        {
            this.gamePlatform.Initialize();
        }

        public void Run()
        {
            this.gamePlatform.Run();
        }

        public void Uninitialize()
        {
            throw new System.NotImplementedException();
        }

        private void ApplicationView_Activated(CoreApplicationView sender, Windows.ApplicationModel.Activation.IActivatedEventArgs args)
        {
            var window = CoreWindow.GetForCurrentThread();
            window.Activate();
        }

        private void CoreApplication_Suspending(object sender, Windows.ApplicationModel.SuspendingEventArgs e)
        {
            SuspendingDeferral deferral = e.SuspendingOperation.GetDeferral();

            this.gamePlatform.Suspend();

            deferral.Complete();
        }

        private void CoreApplication_Resuming(object sender, object e)
        {
            this.gamePlatform.Resume();
        }
    }
}
