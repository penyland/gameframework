// Copyright (c) Peter Nylander.  All rights reserved.

using System.Threading.Tasks;

namespace GameFramework.Contracts
{
    /// <summary>
    /// An interface defining an resource creator.
    /// </summary>
    public interface IResourceLoader
    {
        bool CanLoadResource(string uri);
    }
}
