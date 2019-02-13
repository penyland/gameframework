// Copyright (c) Peter Nylander.  All rights reserved.

using System.Collections.Generic;

namespace GameFramework.Contracts
{
    public interface IRepository<T>
        where T : IEntity
    {
        IEnumerable<T> List { get; }

        void Add(T entity);

        T Find(int id);
    }
}
