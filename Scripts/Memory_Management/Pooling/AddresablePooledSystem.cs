using System;
using System.Collections.Generic;
using System.Diagnostics;
using MECS.Collections;
using MECS.Core;
using MECS.Patrons.Commands;
using MECS.Patrons.ObjectPooling;
using MECS.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace MECS.MemoryManagement.Entity.Pooling
{
    //* System used to manage executions of addresable pooled entities
    //* T = IAddresablePooledData
    [CreateAssetMenu(fileName = "New_Addresable_Pooled_System", menuName = "MECS/Systems/Addresable_Pooled")]
    public class AddresablePooledSystem : ASystem<IAddresablePooledData>, IDisposable
    {
        //ASystem
        //Subscribe to commands
        protected override void OnEnable()
        {
            //ASystem, base execution
            base.OnEnable();

            //TODO: change to to args
            InitializePoolCommand.InitializePoolEvent += InitializePool;
            ReturnPooledEntityCommand.ReturnPooledEntityEvent += ReturnPooledObject;
            SpawnPooledEntityCommand.SpawnPooledEntityEvent += SpawnPooledEntityResponse;
            SceneManager.activeSceneChanged += (oldScene, newScene) => { Dispose(); };
        }

        //ASystem
        //Unsubscribe from commands
        protected override void OnDisable()
        {
            //ASystem, base execution
            base.OnDisable();

            //TODO: change to to args
            InitializePoolCommand.InitializePoolEvent -= InitializePool;
            ReturnPooledEntityCommand.ReturnPooledEntityEvent -= ReturnPooledObject;
            SpawnPooledEntityCommand.SpawnPooledEntityEvent -= SpawnPooledEntityResponse;
            SceneManager.activeSceneChanged -= (oldScene, newScene) => { Dispose(); };

            //Release all the entities
            Dispose();
        }

        //TODO: change to to args
        //Methods
        //Set pool initial values
        private void InitializePool(object sender, AddresableEntityPool pool)
        {
            //Initialize pool if its necessary
            if (!ReferenceTools.IsValueSafe(pool.ObjectPool))
            {
                //Local method, create a new game object from loaded asset reference
                GameObject CreateNewInstance()
                {
                    //Return value
                    GameObject newInstance = null;

                    //Response to asset reference
                    pool.GameObjectAddressablesLoader.LoadedAssetReferenceEvent += SetNewEntity;

                    //Load asset reference
                    pool.GameObjectAddressablesLoader.LoadResource(pool.AssetReference);

                    return newInstance;
                }

                //Local method, release new entities to add these on object pool
                void SetNewEntity(object sender, LoadedAssetReferenceArgs<GameObject> loadArgs)
                {
                    //Remove subscription from event
                    pool.GameObjectAddressablesLoader.LoadedAssetReferenceEvent -= SetNewEntity;

                    //Check loaded result is valid
                    if (loadArgs.asyncOperationHandle.IsValid())
                    {
                        //Store result from load
                        GameObject prefabResult = loadArgs.asyncOperationHandle.Result;
                        GameObject loadedEntity = MonoBehaviour.Instantiate(prefabResult);

                        //Check if entity has necessary component or add value to it
                        if (!loadedEntity.TryGetComponent(out AddresablePooledComponent component))
                        {
                            //Set component, data and release entity
                            component = loadedEntity.AddComponent<AddresablePooledComponent>();
                            SetData();
                        }
                        else
                            //Set data and release entity
                            SetData();

                        //Local method, set data values if its necessary 
                        void SetData()
                        {
                            //Check if should set array
                            if (!ReferenceTools.IsValueSafe(component.Data))
                                component.Data = new AddresablePooledData[1] { new AddresablePooledData() };

                            //Store data
                            AddresablePooledData[] dataArray = component.Data;

                            //Store returned entities, avoid return twice
                            List<GameObject> returnedEntities = new List<GameObject>();

                            //Intenerate to check if should set inside values
                            foreach (AddresablePooledData data in dataArray)
                            {
                                if (!ReferenceTools.IsValueSafe(data.AddresableEntityPool))
                                    data.AddresableEntityPool = pool;

                                if (!ReferenceTools.IsValueSafe(data.AssociatedEntity))
                                    data.AssociatedEntity = loadedEntity;

                                //Return new entity to pool
                                data.IsPooledObject = true;

                                //Avoid return twice
                                if (!returnedEntities.Contains(loadedEntity))
                                {
                                    data.AddresableEntityPool.ObjectPool.ReturnObject(loadedEntity);
                                    returnedEntities.Add(loadedEntity);
                                }
                            }
                        }
                    }
                    //Notify debug manager
                    else
                        new NotificationCommand<DebugArgs>(sender,
                        new DebugArgs(" tried to release a invalid loaded asset reference", LogType.Warning,
                        new System.Diagnostics.StackTrace(true))).Execute();
                }

                //Local method, destroy entity at pool max size capacity
                void ActionOnDestroy(GameObject entity)
                {
                    //Destroy object
                    Addressables.ReleaseInstance(entity);

                    //Avoid errors
                    if (ReferenceTools.IsValueSafe(entity, " tried to destroy a empty entity reference"))
                        MonoBehaviour.Destroy(entity);
                }

                //Local method, check if entity inst null
                void ActionOnGet(GameObject entity) => ReferenceTools.IsValueSafe(entity, " missing reference on Get()");

                //Local method, disable entity on release
                void ActionOnReturn(GameObject entity) => entity.SetActive(false);

                //Set addresable actions
                pool.ObjectPool =
                    new ObjectPool<GameObject>(CreateNewInstance, ActionOnGet, ActionOnReturn, ActionOnDestroy,
                     10, pool.PoolMaxSize, pool.PoolDangerSize, pool.CanReturnEntities, pool.CanExpandPool);
            }
            //Notify debug manager  
            else
                new NotificationCommand<DebugArgs>(sender,
                new DebugArgs(" tried to initialize a already initialized object pool", LogType.Warning,
                new StackTrace(true))).Execute();
        }

        //TODO: change to to args
        //TODO: Add pool initialization if it hasn't
        //Return pooled object to the pool
        private void ReturnPooledObject(object sender, AddresablePooledComponent component)
        {
            //Avoid errors            
            if (CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { sender, component },
            " tried to return pooled entity with null values"))
            {
                //Store entity data and return it
                AddresablePooledData[] entityData = component.Data;
                AddresablePooledData data = entityData[0];

                //Return entity
                if (!data.IsPooledObject)
                {
                    data.IsPooledObject = true;
                    data.AddresableEntityPool.ObjectPool.ReturnObject(data.AssociatedEntity);
                }
                //Notify debug manager
                else
                    new NotificationCommand<DebugArgs>(sender,
                    new DebugArgs(" tried to return an already returned entity", LogType.Warning, new StackTrace(true))).Execute();
            }
        }

        //Response to spawn notification, instantiate a new entity from target pool
        private void SpawnPooledEntityResponse(object sender, SpawnPooledEntityArgs args)
        {
            //Check parameters
            if (ReferenceTools.AreEventParametersValid(sender, args, " given event parameters aren't valid"))
            {
                //Initialize pool if its necessary
                if (ReferenceTools.IsValueSafe(args.addresableEntityPool.ObjectPool,
                args.debugMessage + " tried to spawn from not initialized pool"))
                {
                    //Store reference
                    AddresableEntityPool addresableEntityPool = args.addresableEntityPool;

                    //Spawn object
                    GameObject getEntity = addresableEntityPool.ObjectPool.Get();

                    //Check if could get entity from pool
                    if (ReferenceTools.IsValueSafe(getEntity,
                    args.debugMessage + " got a null reference from pool, try to increase configuration values"))
                    {
                        //Set is pooled value
                        if (getEntity.TryGetComponent<AddresablePooledComponent>(out AddresablePooledComponent component))
                        {
                            //Set not pooled value
                            component.Data[0].IsPooledObject = false;

                            //Set transforms
                            if (addresableEntityPool.SetPositionOnSpawn)
                                getEntity.transform.position = args.spawnTransform.position;

                            if (addresableEntityPool.SetRotationOnSpawn)
                                getEntity.transform.rotation = args.spawnTransform.parent.rotation * args.spawnTransform.localRotation;

                            //Activate
                            getEntity.SetActive(true);
                        }
                        //Notify debug manager
                        else
                            new NotificationCommand<DebugArgs>(sender,
                            new DebugArgs(args.debugMessage + " pooled entity doesnt have AddresablePooledComponent", LogType.Error,
                            new StackTrace(true))).Execute();
                    }
                }
            }
        }

        //IDisposable, used to release all the pools and entities when system is unloaded
        public void Dispose()
        {
            //Itinerate data array and release attached entities and try to unload pool asset reference
            foreach (KeyValuePair<IAddresablePooledData, MonoBehaviour> pair in AddresablePooledManager.dataDictionary.dictionary)
            {
                //Store pool to avoid loose reference
                AddresableEntityPool addresableEntityPool = pair.Key.AddresableEntityPool;
                //Release reference and entity
                addresableEntityPool.GameObjectAddressablesLoader.ReleaseResources(addresableEntityPool.AssetReference);
                addresableEntityPool.ObjectPool.Dispose();
                addresableEntityPool.ObjectPool = null;
            }
        }

        //ASystem, check if data values are valid
        protected override bool IsValidData(Component entityComponent, IAddresablePooledData data) =>
            //Check parameters
            //Check entityComponent parameter
            ReferenceTools.IsValueSafe(entityComponent, " entityComponent parameter isn't safe")

            //Check data parameter
            && ReferenceTools.IsValueSafe(data, " data parameter isn't safe")

            //Check AddresableEntityPool value on data
            && ReferenceTools.IsValueSafe(data.AddresableEntityPool,
            " addresableEntityPool isn't safe on entity: " + entityComponent.gameObject.name);
    }
}