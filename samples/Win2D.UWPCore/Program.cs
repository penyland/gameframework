// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework;
using Windows.ApplicationModel.Core;

namespace Win2D.UWPCore
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CoreApplication.Run(new FrameworkViewSourceFactory<Win2DGameFactory>());
        }
    }
}
