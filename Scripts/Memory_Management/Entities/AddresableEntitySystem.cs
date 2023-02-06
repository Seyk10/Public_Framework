using System.Linq;
using System.Collections.Generic;
using MECS.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;
using MECS.Collections;
using static MECS.Tools.DebugTools;
using MECS.Tools;

namespace MECS.MemoryManagement.Entity
{
    //* System used to manage process related entities loaded from asset references
    //* T = IAddresableEntityData
    [CreateAssetMenu(fileName = "New_Addresable_Entity_System", menuName = "MECS/Systems/Addresable_Entity")]
    public class AddresableEntitySystem : ASystem<IAddresableEntityData>
    {
        //Variables
        private readonly Dictionary<GameObjectAddressablesLoader, Queue<Transform>> loadingProcessDictionary = new();

        //ScriptableObject, subscribe to events
        protected override void OnEnable()
        {
            //ASystem base execution
            base.OnEnable();

            //Response to destroy
            AddresableEntityManager.dataDictionary.ElementRemovedEvent += OnComponentDestroyResponse;
            //Response to editor instantiation
            InstantiateAssetReferenceEntityCommand.InstantiateAssetReference +=
                OnScriptableInstanceResponse;
        }

        //ScriptableObject, unsubscribe from events
        protected override void OnDisable()
        {
            //ASystem base execution
            base.OnDisable();

            //Response to destroy
            AddresableEntityManager.dataDictionary.ElementRemovedEvent -= OnComponentDestroyResponse;
            //Response to editor instantiation
            InstantiateAssetReferenceEntityCommand.InstantiateAssetReference -=
                OnScriptableInstanceResponse;
        }

        //Response to instantiation event from scriptable command
        private void OnScriptableInstanceResponse(object sender, InstantiateAssetReferenceEventArgs args)
        {
            //Variables
            bool canSubscribe = false;
            GameObjectAddressablesLoader gameObjectAddressablesLoader =
                args.gameObjectAddressablesLoader;
            Transform spawnPosition = args.spawnPoint;

            //Local method, store on dictionary the new loading data
            void StoreLoadingData()
            {
                //Store loading process
                if (!loadingProcessDictionary.ContainsKey(gameObjectAddressablesLoader))
                {
                    canSubscribe = true;

                    //Create a new list with data
                    Queue<Transform> value = new();
                    value.Enqueue(spawnPosition);

                    loadingProcessDictionary.Add(gameObjectAddressablesLoader, value);
                }
                //Add new position to value
                else
                    loadingProcessDictionary[gameObjectAddressablesLoader].Enqueue(spawnPosition);
            }

            //Store data
            StoreLoadingData();

            //Subscribe to asset loaded event
            if (canSubscribe)
                gameObjectAddressablesLoader.LoadedAssetReferenceEvent += OnAssetLoadedResponse;

            //Start loading
            args.gameObjectAddressablesLoader.LoadResource(args.assetReference);
        }

        //Instantiate loaded operations
        private void OnAssetLoadedResponse(object sender, LoadedAssetReferenceArgs<GameObject> args)
        {
            //Variables
            GameObjectAddressablesLoader gameObjectAddressablesLoader =
                (GameObjectAddressablesLoader)sender;

            //Avoid errors
            if (loadingProcessDictionary.ContainsKey(gameObjectAddressablesLoader))
            {
                //Variables
                //Remove subscription if this is the last loading process with this loader
                bool removeSubscription =
                    loadingProcessDictionary[gameObjectAddressablesLoader].Count == 1;
                Transform spawnPosition = loadingProcessDictionary[
                    gameObjectAddressablesLoader
                ].Dequeue();

                //Local method, remove subscription from loader and remove from dictionary
                void CheckRemoveLoader()
                {
                    //Check if can remove
                    if (removeSubscription)
                    {
                        loadingProcessDictionary.Remove(gameObjectAddressablesLoader);
                        gameObjectAddressablesLoader.LoadedAssetReferenceEvent -=
                            OnAssetLoadedResponse;
                    }
                }

                //Local method, instantiate a new entity with operation result
                void InstantiateNewEntity()
                {
                    //Only process response if load is valid
                    if (args.asyncOperationHandle.IsValid())
                    {
                        //Store new entity
                        GameObject newAddresableEntity = Instantiate(args.asyncOperationHandle.Result);

                        //Local method, set entity values
                        void SetEntityValues(AddresableEntityComponent component)
                        {
                            //Store value from component
                            AddresableEntityData[] arrayData = component.DataReference.GetValue();

                            //Convert value to list
                            List<AddresableEntityData> componentDataList =
                                arrayData != null ? arrayData.ToList() : new();

                            componentDataList.Add(
                                new AddresableEntityData(
                                    args.assetReference,
                                    gameObjectAddressablesLoader,
                                    newAddresableEntity
                                )
                            );

                            //Set new data
                            if (component.DataReference.useLocalValues)
                                component.DataReference.localValue = componentDataList.ToArray();
                            else
                                component.DataReference.externalValue.Data =
                                    componentDataList.ToArray();

                            //Set transform values to new instance
                            newAddresableEntity.transform.position = spawnPosition
                                .transform
                                .position;
                            newAddresableEntity.transform.rotation = spawnPosition
                                .transform
                                .rotation;
                        }

                        //Set component values
                        if (
                            newAddresableEntity.TryGetComponent(
                                out AddresableEntityComponent component
                            )
                        )
                            SetEntityValues(component);
                        else
                        {
                            component =
                                newAddresableEntity.AddComponent<AddresableEntityComponent>();
                            SetEntityValues(component);
                        }

                        //! Check if pooled object
                        //And release resource
                        //Store data to work with
                        //Local method, release resource on data
                        void ReleaseDataResources()
                        {
                            AddresableEntityData[] dataArray = component.DataReference.GetValue();

                            bool canUnload = true;

                            //Avoid instances created without loader
                            foreach (var data in dataArray)
                                if (data.IResourceLoader == null)
                                {
                                    canUnload = false;
                                    break;
                                }

                            //Unload asset reference
                            if (canUnload)
                                foreach (var data in dataArray)
                                    data.IResourceLoader.ReleaseResources(data.AssetReference);
                            else
                                Debug.LogWarning(
                                    "Warning: Cant unload asset reference from entities which weren't instantiated without a loader"
                                );
                        }

                        //Code execution
                        ReleaseDataResources();
                    }
                }

                //Executions
                InstantiateNewEntity();
                CheckRemoveLoader();
            }
            else
                Debug.Log(
                    "Warning: Tried to instantiate from loader event and missing loading process"
                );
        }

        //Response to component about to be destroyed
        private void OnComponentDestroyResponse(object sender, ResponsiveDictionaryArgs<IAddresableEntityData, MonoBehaviour> args)
        {
            //Release instances
            if (Addressables.ReleaseInstance(args.key.Entity))
                Debug.LogWarning(
                    "Warning: Tried to release entity not loaded from asset reference on "
                        + sender
                        + ", the entity tried to unload was "
                        + args.key.Entity.name
                );
        }

        //ASystem implementation used to check data values
        protected override bool IsValidData(Component entity, IAddresableEntityData data,
        ComplexDebugInformation complexDebugInformation)
        {
            //Debug values
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name,
             "IsValidData(Component entity, IAddresableEntityData data)");
            string entityName = entity.gameObject.name;

            return
            //Check parameters
            //Check entity
            ReferenceTools.IsValueSafe(entity,
            new ComplexDebugInformation(basicDebugInformation, "given entity isn't safe"))

            //Check data
            && ReferenceTools.IsValueSafe(data,
            new ComplexDebugInformation(basicDebugInformation, "given data isn't safe"))

            //Check asset reference
            && ReferenceTools.IsValueSafe(data.AssetReference,
            complexDebugInformation.AddTempCustomText("asset reference isn't safe on entity: " + entityName))

            //Check resource loader
            && ReferenceTools.IsValueSafe(data.IResourceLoader,
            complexDebugInformation.AddTempCustomText("IResourceLoader isn't safe on entity: " + entityName))

            //Check entity value
            && ReferenceTools.IsValueSafe(data.Entity,
            complexDebugInformation.AddTempCustomText("entity on data isn't safe on entity: " + entityName));
        }
    }
}