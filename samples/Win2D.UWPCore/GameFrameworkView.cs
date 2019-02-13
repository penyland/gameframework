// Copyright (c) Peter Nylander.  All rights reserved.

using System.Diagnostics;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace Win2D.UWPCore
{
    public class GameFrameworkView : IFrameworkView
    {
        private Window window;

        public void Initialize(CoreApplicationView applicationView)
        {
            Debug.WriteLine("Initialize");
            applicationView.Activated += this.OnActivated;
            CoreApplication.Suspending += this.CoreApplication_Suspending;
        }

        public void SetWindow(CoreWindow coreWindow)
        {
            Debug.WriteLine("SetWindow");

            // TitleBarManager.ExtendViewIntoTitleBar(true);

            this.window = new Window(coreWindow);
        }

        public void Load(string entryPoint)
        {
            Debug.WriteLine("Load");
        }

        public void Run()
        {
            Debug.WriteLine("Run");
            this.window.Run();
        }

        public void Uninitialize()
        {
            throw new System.NotImplementedException();
        }

        private void OnActivated(CoreApplicationView sender, Windows.ApplicationModel.Activation.IActivatedEventArgs args)
        {
            Debug.WriteLine("ApplicationView_Activated");
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
