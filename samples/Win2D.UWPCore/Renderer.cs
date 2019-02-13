// Copyright (c) Peter Nylander.  All rights reserved.

using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Text;
using System;
using System.Diagnostics;
using System.Numerics;
using Windows.Gaming.Input;
using Windows.UI;
using Windows.UI.Core;

namespace Win2D.UWPCore
{
    public class Renderer
    {
        private readonly CanvasDevice canvasDevice;
        private Windows.Foundation.Rect bounds;
        private readonly CoreWindow coreWindow;
        private SwapChainManager swapChainManager;
        private GamepadManager gamepadManager;

        public Renderer(Windows.Foundation.Rect bounds, CoreWindow coreWindow)
        {
            this.canvasDevice = new CanvasDevice();
            this.canvasDevice.DeviceLost += this.CanvasDevice_DeviceLost;

            this.bounds = bounds;
            this.coreWindow = coreWindow;
            this.swapChainManager = new SwapChainManager(this.coreWindow, this.canvasDevice);
            this.gamepadManager = new GamepadManager();
        }

        public void Initialize(Windows.Foundation.Rect bounds)
        {
            this.bounds = bounds;
            this.swapChainManager.EnsureMatchesWindow(bounds);
        }

        public void Update()
        {
        }

        public void Render()
        {
            CanvasSwapChain swapChain = this.swapChainManager.SwapChain;

            using (var ds = swapChain.CreateDrawingSession(Colors.CornflowerBlue))
            {
                this.DrawInfo(ds);
                this.DrawGamepadState(ds);
            }

            swapChain.Present();
        }

        private void CanvasDevice_DeviceLost(CanvasDevice sender, object args)
        {
            Debug.WriteLine("CanvasDevice_DeviceLost");
        }

        private void DrawGamepadState(CanvasDrawingSession ds)
        {
            Gamepad gamepad = this.gamepadManager.GetFirstGamepad();
            if (gamepad != null)
            {
                Windows.Gaming.Input.GamepadReading currentReading = gamepad.GetCurrentReading();
                var buttons = currentReading.Buttons;

                if (buttons > 0)
                {
                    // A button was pressed
                    ds.DrawText("A button was pressed", new Vector2(400, 40), Colors.White);
                }

                string message = string.Format("A={0}", buttons.HasFlag(GamepadButtons.A));

                if ((buttons & GamepadButtons.A) == GamepadButtons.A)
                {
                    ds.DrawText("GamepadButtons.A pressed", new Vector2(400, 60), Colors.Green);
                }

                ds.DrawText(message, new Vector2(100, 200), Colors.White);

                // Draw battery info
                var batteryInfo = gamepad.TryGetBatteryReport();
                var batteryString = $"Gamepad 1 - Battery status: {batteryInfo.Status.ToString()} - Remaining capacity {batteryInfo.RemainingCapacityInMilliwattHours} mWh " +
                    $"- Full capacity {batteryInfo.FullChargeCapacityInMilliwattHours} mWh";

                ds.DrawText(batteryString, new Vector2(0, 0), Colors.White);
            }
            else
            {
                ds.DrawText("No controller attached", new Vector2(0, 0), Colors.Red);
            }
        }

        private void DrawInfo(CanvasDrawingSession ds)
        {
            var swapChain = this.swapChainManager.SwapChain;
            var size = swapChain.Size;

            var message = string.Format("{0:00}x{1:00} @{2:00}dpi", size.Width, size.Height, swapChain.Dpi);

            if (swapChain.Rotation != CanvasSwapChainRotation.None)
            {
                message += " " + swapChain.Rotation.ToString();
            }

            ds.DrawText(
                message,
                new Vector2(100, 100),
                //size.ToVector2(),
                Colors.White,
                new CanvasTextFormat()
                {
                    FontSize = 12,
                    HorizontalAlignment = CanvasHorizontalAlignment.Right,
                    VerticalAlignment = CanvasVerticalAlignment.Bottom,
                });
        }
    }
}
