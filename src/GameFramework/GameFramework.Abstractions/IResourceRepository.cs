// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameFramework.Abstractions
{
    public interface IResourceRepository
    {
        Dictionary<string, ITexture> Sprites { get; }

        void Load(string key, string uri);

        Task LoadAsync(string key, string uri);
    }
}
