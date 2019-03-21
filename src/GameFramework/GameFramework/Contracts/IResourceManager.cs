// Copyright (c) Peter Nylander.  All rights reserved.

using System.Threading.Tasks;

namespace GameFramework.Contracts
{
    public interface IResourceManager
    {
        void AddResourceLoader(IResourceLoader resourceLoader);

        Task<T> LoadAsync<T>(string uri);
    }
}
