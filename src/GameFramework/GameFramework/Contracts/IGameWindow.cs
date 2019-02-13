// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Collections.Generic;
using System.Text;

namespace GameFramework.Contracts
{
    public interface IGameWindow
    {
        void OnActivated();

        void Run();
    }
}
