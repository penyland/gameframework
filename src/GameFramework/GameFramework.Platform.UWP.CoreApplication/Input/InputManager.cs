// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Windows.UI.Input;
using Windows.UI.Xaml;

namespace GameFramework.Platform.Input
{
    public class InputManager
    {
        public InputManager(UIElement inputElement)
        {
            if (inputElement != null)
            {
                inputElement.PointerPressed += this.UIElement_PointerPressed;
            }
        }

        private void UIElement_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            this.PointerPressed(e.GetCurrentPoint(null));
            e.Handled = true;
        }

        private void PointerPressed(PointerPoint pointerPoint)
        {
        }

        private void PointerMoved()
        {
        }

        private void PointerReleased()
        {
        }
    }
}
