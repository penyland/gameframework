// Copyright (c) Peter Nylander.  All rights reserved.

using System.Diagnostics;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.Core;
using Windows.UI.Popups;

namespace Win2D.UWPCore
{
    public class Window
    {
        private CoreWindow coreWindow;

        private bool windowClosed = false;
        private Renderer renderer;
        private Rect bounds;

        public Window(CoreWindow coreWindow)
        {
            this.coreWindow = coreWindow;

            // Size
            this.bounds = this.coreWindow.Bounds;
            this.coreWindow.Activated += this.CoreWindow_Activated;
            this.coreWindow.SizeChanged += this.OnSizeChanged;
            this.coreWindow.Closed += this.OnClosed;
            this.coreWindow.VisibilityChanged += (s, e) => { Debug.WriteLine($"Window::VisibilityChanged {e.Visible.ToString()}"); };
            this.coreWindow.ResizeStarted += (s, e) => { Debug.WriteLine($"Window::ResizeStarted"); };
            this.coreWindow.ResizeCompleted += (s, e) => { Debug.WriteLine($"Window::ResizeCompleted"); };

            // Input handling
            this.coreWindow.CharacterReceived += this.CoreWindow_CharacterReceived;
            this.coreWindow.InputEnabled += (s, e) => { Debug.WriteLine($"Window::InputEnabled {e.InputEnabled}"); };
            this.coreWindow.TouchHitTesting += (s, e) => { Debug.WriteLine($"Window::TouchHitTesting {e.ToString()}"); };
            this.coreWindow.PointerPressed += this.OnPointerPressed;
            this.coreWindow.KeyDown += this.OnKeyDown;
            this.coreWindow.KeyUp += this.OnKeyUp;

            // threading
            var dispatcher = this.coreWindow.Dispatcher;
            dispatcher.AcceleratorKeyActivated += this.OnAcceleratorKeyActivated;

            DisplayInformation.DisplayContentsInvalidated += (s, e) => { Debug.WriteLine($"SwapChainManager::DisplayInformation.DisplayContentsInvalidated"); };
            var displayInformation = DisplayInformation.GetForCurrentView();
            displayInformation.DpiChanged += (s, e) => { Debug.WriteLine($"DpiChanged {displayInformation.LogicalDpi}"); };

            this.renderer = new Renderer(this.bounds, coreWindow);
        }

        public void Run()
        {
            this.renderer.Initialize(this.bounds);

            var window = CoreWindow.GetForCurrentThread();

            while (!this.windowClosed)
            {
                window.Dispatcher.ProcessEvents(CoreProcessEventsOption.ProcessAllIfPresent);

                this.renderer.Update();
                this.renderer.Render();
            }
        }

        private void OnAcceleratorKeyActivated(CoreDispatcher sender, AcceleratorKeyEventArgs e)
        {
            Debug.WriteLine($"Window::Dispatcher.AcceleratorKeyActivated {e.EventType.ToString()}");
            Debug.WriteLine($"Window::Dispatcher.AcceleratorKeyActivated {e.VirtualKey.ToString()}");

            // Left shift and right shift
            var virtualKey = e.VirtualKey;
            if (virtualKey == Windows.System.VirtualKey.Shift)
            {
                if (e.EventType == CoreAcceleratorKeyEventType.SystemKeyDown ||
                    e.EventType == CoreAcceleratorKeyEventType.KeyDown)
                {
                    // Key down
                    if (e.KeyStatus.ScanCode == 0x36)
                    {
                        Debug.WriteLine($"{e.KeyStatus.ScanCode} - Right shift");
                    }
                    else
                    {
                        Debug.WriteLine($"{e.KeyStatus.ScanCode} - Left shift");
                    }
                }
            }
        }

        private void CoreWindow_Activated(CoreWindow sender, WindowActivatedEventArgs args)
        {
            Debug.WriteLine($"Window::Activated {args.WindowActivationState.ToString()}");
        }

        private void OnSizeChanged(CoreWindow sender, WindowSizeChangedEventArgs args)
        {
            Debug.WriteLine($"Window::OnSizeChanged {args.Size.ToString()}");
            this.bounds = new Rect(0, 0, args.Size.Width, args.Size.Height);
            this.renderer.Initialize(this.bounds);
        }

        private void OnClosed(CoreWindow sender, CoreWindowEventArgs args)
        {
            Debug.WriteLine($"Window::Closed");
            this.windowClosed = true;
        }

        private void OnKeyUp(CoreWindow sender, KeyEventArgs e)
        {
            Debug.WriteLine($"Window::OnKeyUp VirtualKey = {e.VirtualKey}");
            Debug.WriteLine($"Window::OnKeyUp DeviceId = {e.DeviceId}");

            if (e.VirtualKey == Windows.System.VirtualKey.A)
            {
            }
        }

        private void OnKeyDown(CoreWindow sender, KeyEventArgs e)
        {
            Debug.WriteLine($"Window::OnKeyDown VirtualKey = {e.VirtualKey}");
            Debug.WriteLine($"Window::OnKeyDown DeviceId = {e.DeviceId}");

            if (e.VirtualKey == Windows.System.VirtualKey.A)
            {
            }
        }

        private void OnPointerPressed(CoreWindow sender, PointerEventArgs args)
        {
            MessageDialog messageDialog = new MessageDialog("PointerPressed");
            messageDialog.ShowAsync();
        }

        private void CoreWindow_CharacterReceived(CoreWindow sender, CharacterReceivedEventArgs e)
        {
            Debug.WriteLine($"Window::CharacterReceived KeyCode = {e.KeyCode}");
            this.DisplayKeyStatus("CharacterReceived", e.KeyStatus);
        }

        private void DisplayKeyStatus(string sender, CorePhysicalKeyStatus keyStatus)
        {
            Debug.WriteLine($"Window::{sender} KeyStatus.IsExtendedKey = {keyStatus.IsExtendedKey}");
            Debug.WriteLine($"Window::{sender} KeyStatus.IsKeyReleased = {keyStatus.IsKeyReleased}");
            Debug.WriteLine($"Window::{sender} KeyStatus.IsMenuKeyDown = {keyStatus.IsMenuKeyDown}");
            Debug.WriteLine($"Window::{sender} KeyStatus.RepeatCount = {keyStatus.RepeatCount}");
            Debug.WriteLine($"Window::{sender} KeyStatus.ScanCode = {keyStatus.ScanCode}");
            Debug.WriteLine($"Window::{sender} KeyStatus.WasKeyDown = {keyStatus.WasKeyDown}");
            Debug.WriteLine(string.Empty);
        }
    }
}
