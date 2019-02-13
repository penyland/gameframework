// Copyright (c) Peter Nylander.  All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameFramework.Contracts
{
    public interface IResourceRepository
    {
        Dictionary<string, ITexture> Sprites { get; }

        void Load(string key, string uri);

        Task LoadAsync(string key, string uri);
    }
}
