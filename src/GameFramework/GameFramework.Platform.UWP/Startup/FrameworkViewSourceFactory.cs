// Copyright (c) Peter Nylander.  All rights reserved.

using GameFramework.Contracts;
using Windows.ApplicationModel.Core;

namespace GameFramework
{
    /// <summary>
    /// Game framework factory.
    /// </summary>
    /// <typeparam name="T">The factory type that creates an instance of IGame.</typeparam>
    public class FrameworkViewSourceFactory<T> : IFrameworkViewSource
        where T : IGameFactory, new()
    {
        public IFrameworkView CreateView()
        {
            return new GameFrameworkView<T>();
        }
    }
}
