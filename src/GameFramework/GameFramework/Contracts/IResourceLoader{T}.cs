// Copyright (c) Peter Nylander.  All rights reserved.

using System.Threading.Tasks;

namespace GameFramework.Contracts
{
    /// <summary>
    /// An interface defining an resource creator.
    /// </summary>
    public interface IResourceLoader<T>
    {
        bool CanLoadResource(string uri);

        /// <summary>
        /// Creates a resource.
        /// </summary>
        /// <param name="uri">Uri to resource.</param>
        /// <returns>A resource.</returns>
        Task<T> LoadResourceAsync(string uri);
    }
}
