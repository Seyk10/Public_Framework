using System;
using System.Collections.Generic;
using MECS.Core;
using MECS.Patrons.ObjectPooling;
using MECS.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using static MECS.Tools.DebugTools;

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

            InitializePoolCommand.InitializePoolEvent -= InitializePool;
            ReturnPooledEntityCommand.ReturnPooledEntityEvent -= ReturnPooledObject;
            SpawnPooledEntityCommand.SpawnPooledEntityEvent -= SpawnPooledEntityResponse;
            SceneManager.activeSceneChanged -= (oldScene, newScene) => { Dispose(); };

            //Release all the entities
            Dispose();
        }

        //Methods
        //Set pool initial values
        private void InitializePool(object sender, AddresableEntityPool pool)
        {
            //Check if given pool is initialized
            bool emptyPool = pool.ObjectPool == null;

            //Initialize pool if its necessary
            if (emptyPool)
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
                            if (component.DataReference.GetValue() == null)
                                component.DataReference.localValue = new AddresablePooledData[1] { new AddresablePooledData() };

                            //Store data
                            AddresablePooledData[] dataArray = component.DataReference.GetValue();

                            //Store returned entities, avoid return twice
                            List<GameObject> returnedEntities = new List<GameObject>();

                            //Intenerate to check if should set inside values
                            foreach (AddresablePooledData data in dataArray)
                            {
                                if (data.AddresableEntityPool == null)
                                    data.AddresableEntityPool = pool;

                                if (data.AssociatedEntity == null)
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
                    else
                        Debug.LogWarning("Warning: Tried to release a invalid loaded asset reference");
                }

                //Local method, destroy entity at pool max size capacity
                void ActionOnDestroy(GameObject entity)
                {
                    //Destroy object
                    Addressables.ReleaseInstance(entity);

                    //Avoid errors
                    if (entity != null)
                        MonoBehaviour.Destroy(entity);
#if UNITY_EDITOR
                    else
                        Debug.LogWarning("Warning:  Tried to destroy a empty entity reference");
#endif
                }

                //Local method, check if entity inst null
                void ActionOnGet(GameObject entity)
                {
#if UNITY_EDITOR
                    //Avoid missing references
                    if (entity == null)
                        Debug.LogWarning("Warning: Missing reference on Get()");
#endif
                }

                //Local method, disable entity on release
                void ActionOnReturn(GameObject entity) => entity.SetActive(false);

                //Set addresable actions
                pool.ObjectPool =
                    new ObjectPool<GameObject>(CreateNewInstance, ActionOnGet, ActionOnReturn, ActionOnDestroy,
                     10, pool.PoolMaxSize, pool.PoolDangerSize, pool.CanReturnEntities, pool.CanExpandPool);
            }
            //Debug errors
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: Tried to initialize a already initialized object pool.");
#endif
        }

        //Return pooled object to the pool
        private void ReturnPooledObject(object sender, AddresablePooledComponent component)
        {
            bool missingValues = sender == null || component == null;

            //Avoid errors            
            if (!missingValues)
            {
                //Store entity data and return it
                AddresablePooledData[] entityData = component.DataReference.GetValue();
                AddresablePooledData data = entityData[0];

                //Check if cna return current entity
                bool canReturn = !data.IsPooledObject;

                //Return entity
                if (canReturn)
                {
                    data.IsPooledObject = true;
                    data.AddresableEntityPool.ObjectPool.ReturnObject(data.AssociatedEntity);
                }
#if UNITY_EDITOR
                else Debug.LogWarning("Warning: Tried to return an already returned entity");
#endif  
            }
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: Tried to return pooled entity with null values.");
#endif
        }

        //Response to spawn notification, instantiate a new entity from target pool
        private void SpawnPooledEntityResponse(object sender, SpawnPooledEntityArgs args)
        {
            //Variables
            //Store reference
            AddresableEntityPool addresableEntityPool = args.addresableEntityPool;
            //Check if given pool is initialized
            bool emptyPool = addresableEntityPool.ObjectPool == null;

            //Initialize pool if its necessary
            if (!emptyPool)
            {
                //Spawn object
                GameObject getEntity = addresableEntityPool.ObjectPool.Get();

                if (getEntity != null)
                {
                    //Set is pooled value
                    if (getEntity.TryGetComponent<AddresablePooledComponent>(out AddresablePooledComponent component))
                    {
                        //Set not pooled value
                        component.DataReference.GetValue()[0].IsPooledObject = false;

                        //Set transforms
                        if (addresableEntityPool.SetPositionOnSpawn)
                            getEntity.transform.position = args.spawnTransform.position;

                        if (addresableEntityPool.SetRotationOnSpawn)
                            getEntity.transform.rotation = args.spawnTransform.parent.rotation * args.spawnTransform.localRotation;

                        //Activate
                        getEntity.SetActive(true);
                    }
#if UNITY_EDITOR
                    else Debug.LogWarning("Warning: Pooled entity doesnt have AddresablePooledComponent");
#endif
                }
#if UNITY_EDITOR
                else Debug.LogWarning("Warning: Got a null reference from pool, try to increase configuration values");
#endif
            }
#if UNITY_EDITOR
            else Debug.LogWarning("Warning: Tried to spawn from not initialized pool");
#endif
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
        protected override bool IsValidData(Component entityComponent, IAddresablePooledData data,
        ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
             "IsValidData(Component entity, IAddresablePooledData data)");
            string entityName = entityComponent.gameObject.name;

            return
            //Check parameters
            //Check entityComponent parameter
            ReferenceTools.IsValueSafe(entityComponent, new ComplexDebugInformation(basicDebugInformation,
            "entityComponent parameter isn't safe"))

            //Check data parameter
            && ReferenceTools.IsValueSafe(data, new ComplexDebugInformation(basicDebugInformation,
            "data parameter isn't safe"))

            //Check AddresableEntityPool value on data
            && ReferenceTools.IsValueSafe(data.AddresableEntityPool, complexDebugInformation
            .AddTempCustomText("addresableEntityPool isn't safe on entity: " + entityName));
        }
    }
}