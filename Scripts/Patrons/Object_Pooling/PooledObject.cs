using System;
using UnityEngine;

namespace MECS.Patrons.ObjectPooling
{
    //* Class used to store pool information, to manage release and come back to pool of the object
    //* T = type to pool
    public class PooledObject<T> : IDisposable where T : class
    {
        //Variables
        //Store object pooled
        private readonly T pooledObject = default;
        private readonly IObjectPool<T> objectPool = default;

        //Default builder
        public PooledObject(T pooledObject, IObjectPool<T> objectPool)
        {
            this.pooledObject = pooledObject;
            this.objectPool = objectPool;
        }

        //On object dispose, try to release it
        public void Dispose() => objectPool.ReturnObject(pooledObject);
    }
}