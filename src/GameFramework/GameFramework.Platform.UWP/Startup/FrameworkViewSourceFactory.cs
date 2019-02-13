// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Windows.ApplicationModel.Core;

namespace GameFramework
{
    public class FrameworkViewSourceFactory<T> : IFrameworkViewSource
        where T : IGameFactory, new()
    {
        public IFrameworkView CreateView()
        {
            return new GameFrameworkView<T>();
        }
    }
}
