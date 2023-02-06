using System;
using System.Collections.Generic;
using UnityEngine;

namespace MECS.Patrons.ObjectPooling
{
    //* Generic pool used to create, store and track object on a controlled instantiation way
    //* T = type to pool
    public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        //Variables
        //Store all the objects used on object pool
        internal Queue<T> poolQueue = null;

        //Function to raise when object is created
        private readonly Func<T> createFunc = null;

        //Delegate to raise when object is get
        private readonly Action<T> actionOnGet = null,
            //Delegate to raise when object return to pool
            actionOnReturn = null,
            //Delegate to raise on object destroy
            actionOnDestroy = null;

        //Max permitted size on pool
        private readonly int maxSize = 0;

        //Allow to return pooled object to pool
        internal bool collectionCheck = false,
        //Allow expand pool size
        canExpandPool = true;

        //Make spawn objects if pool reached number
        internal int poolDangerSize = 0;

        //The general count of objects
        private int countAll = 0;

        //Attributes
        public int CountInactive => poolQueue.Count;
        public int CountActive => countAll - CountInactive;
        public int CountAll => countAll;

        //Default builder
        public ObjectPool(
            Func<T> createFunc,
            Action<T> actionOnGet = null,
            Action<T> actionOnReturn = null,
            Action<T> actionOnDestroy = null,
            int defaultCapacity = 10,
            int maxSize = 100,
            int poolDangerSize = 10,
            bool collectionCheck = true,
            bool canExpandPool = true
        )
        {
            //Store checks
            bool isMissingCreateFunc = createFunc == null;

            //Debug missing references
#if UNITY_EDITOR
            if (isMissingCreateFunc)
                Debug.LogWarning("Warning: Missing func value on object pool " + nameof(createFunc));
#endif

            this.createFunc = createFunc;
            this.actionOnGet = actionOnGet;
            this.actionOnReturn = actionOnReturn;
            this.actionOnDestroy = actionOnDestroy;

            //Debug wrong pool size
            if (maxSize <= 0)
#if UNITY_EDITOR
                Debug.LogWarning("Warning: Pool size must be bigger than 0, default will me used");
#endif

            this.maxSize = maxSize;
            this.poolDangerSize = poolDangerSize;
            this.canExpandPool = canExpandPool;
            this.collectionCheck = collectionCheck;
            poolQueue = new Queue<T>(defaultCapacity);

            //Create initial elements based on maxSize
            if (!isMissingCreateFunc)
                for (int index = 0; index <= maxSize - 1; index++)
                {
                    countAll++;
                    createFunc();
                }
        }

        //Return object of target type on pool, create and add it if its necessary
        public T Get()
        {
            //Return object
            T returnValue = default;

            //Create object
            if (canExpandPool)
            {
                //Get a return value or create a new one
                while (returnValue == default || returnValue == null)
                {
                    if (poolQueue.Count <= poolDangerSize)
                    {
                        //Avoid errors

                        if (createFunc == null)
                            if (createFunc != null)
                            {
                                //Create new objects
                                for (int index = 0; index <= poolDangerSize - 1; index++)
                                {
                                    //Update count
                                    countAll++;
                                    createFunc();
                                }

                                //Avoid errors
                                if (poolQueue.Count != 0)
                                    returnValue = poolQueue.Dequeue();
#if UNITY_EDITOR
                                else

                                    Debug.LogWarning("Warning: Tried to dequeue from empty pool, increase pool danger size value.");
#endif

                            }
#if UNITY_EDITOR
                            else
                                Debug.LogWarning("Warning: Missing func value on object pool " + nameof(createFunc));
#endif
                    }
                    else
                        returnValue = poolQueue.Dequeue();
                }
            }
#if UNITY_EDITOR
            else
                Debug.LogWarning("Warning: Pool stack limit reached");
#endif


            //Invoke action on get
            actionOnGet?.Invoke(returnValue);

            return returnValue;
        }

        //Return a pooled object with object pool referenced and out of value
        public PooledObject<T> Get(out T value) =>
            new PooledObject<T>(value = Get(), (IObjectPool<T>)this);

        //Return object to pool
        public void ReturnObject(T element)
        {
            //Check if object can be released
            bool hasBeenReleased = collectionCheck && poolQueue.Count > 0 && poolQueue.Contains(element);

            //Debug if cant release
            if (!hasBeenReleased)
            {
                bool returnPooledEntity = CountInactive < maxSize,
                destroyPooledEntity = CountInactive >= maxSize;

                //Check if can push to stack or should destroy object
                if (returnPooledEntity)
                {
                    //Invoke action release if can
                    actionOnReturn?.Invoke(element);
                    poolQueue.Enqueue(element);
                }
                else if (destroyPooledEntity)
                {
                    actionOnDestroy?.Invoke(element);
                    countAll--;
                }
#if UNITY_EDITOR
                else
                    Debug.Log("Warning: Unexpected error trying to release object");
#endif
            }
#if UNITY_EDITOR
            else
                Debug.LogWarning("Warning: Trying to return an object that has already been returned.");
#endif
        }

        //Clear the content of stack
        public void Clear()
        {
            //Itinerate and invoke destroy action
            foreach (T value in poolQueue)
                actionOnDestroy?.Invoke(value);

            poolQueue.Clear();
            countAll = 0;
        }

        //Clear stack values on dispose
        public void Dispose() => Clear();
    }
}