// Copyright (c) Peter Nylander.  All rights reserved.

using Windows.ApplicationModel.Core;

namespace Win2D.UWPCore
{
    public class FrameworkViewSourceFactory : IFrameworkViewSource
    {
        public IFrameworkView CreateView()
        {
            return new GameFrameworkView();
        }
    }
}
