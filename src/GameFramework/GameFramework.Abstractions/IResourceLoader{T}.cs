// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace GameFramework.Abstractions
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
