// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace GameFramework.Abstractions
{
    public interface IResourceManager
    {
        void AddResourceLoader(IResourceLoader resourceLoader);

        Task<T> LoadAsync<T>(string uri);
    }
}
