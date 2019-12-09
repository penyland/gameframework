// Copyright (c) Peter Nylander. All rights reserved.
//
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Threading.Tasks;

namespace GameFramework.Abstractions
{
    /// <summary>
    /// An interface defining an resource creator.
    /// </summary>
    public interface IResourceLoader
    {
        bool CanLoadResource(string uri);
    }
}
