// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;

namespace GameFramework.Platform.Utils
{
    public static class TitleBarManager
    {
        public static void ExtendViewIntoTitleBar(bool extendViewIntoTitleBar)
        {
            CoreApplication.GetCurrentView().TitleBar.ExtendViewIntoTitleBar = extendViewIntoTitleBar;

            if (extendViewIntoTitleBar)
            {
                ApplicationViewTitleBar titleBar = ApplicationView.GetForCurrentView().TitleBar;
                titleBar.ButtonBackgroundColor = Colors.Transparent;
                titleBar.ButtonInactiveBackgroundColor = Colors.Transparent;
            }
        }
    }
}
