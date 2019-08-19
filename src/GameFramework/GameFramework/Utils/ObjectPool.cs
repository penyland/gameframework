// Copyright (c) Peter Nylander.  All rights reserved.

using System;
using System.Collections.Concurrent;

namespace GameFramework.Utils
{
    public class ObjectPool<T>
    {
        private readonly ConcurrentBag<T> objects;
        private readonly Func<T> objectFactory;

        public ObjectPool(Func<T> objectFactory)
        {
            this.objectFactory = objectFactory ?? throw new ArgumentNullException(nameof(objectFactory));
            this.objects = new ConcurrentBag<T>();
        }

        public T Get()
        {
            if (this.objects.TryTake(out T item))
            {
                return item;
            }

            return this.objectFactory();
        }

        public void Put(T item)
        {
            this.objects.Add(item);
        }
    }
}
