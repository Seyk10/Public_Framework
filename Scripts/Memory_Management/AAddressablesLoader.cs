using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using MECS.Collections;
using static MECS.Tools.DebugTools;

namespace MECS.MemoryManagement
{
    //* Class used to store references on assets loaded, their handles and manage their load and release operations
    public abstract class AAddressablesLoader<T> : IDisposable, IResourceLoader<AssetReference>
    {
        //Variables
        //Store loaded assets
        private readonly List<AssetReference> loadedAssets = new();

        //Store assets that are loading
        private readonly List<AssetReference> queuedAssets = new();

        //Store results of loading operations
        private readonly Dictionary<
            AssetReference,
            AsyncOperationHandle<T>
        > asyncOperationHandlesDictionary = new();

        //Attributes
        public AssetReference[] LoadedResources => loadedAssets.ToArray();
        public AssetReference[] QueuedResources => queuedAssets.ToArray();

        //Event, notify when reference is loaded
        public event EventHandler<LoadedAssetReferenceArgs<T>> LoadedAssetReferenceEvent = null;

        //Methods
        //Load asset reference and handle its events
        public virtual void LoadResource(AssetReference value)
        {
            //Avoid repeat operations
            if (!asyncOperationHandlesDictionary.ContainsKey(value))
            {
                //Store as loading asset
                CollectionsTools.listTools.AddValue(queuedAssets, value,
                new ComplexDebugInformation(this.GetType().Name, "LoadResource(AssetReference value)", "couldnt add value to queuedAssets"));

                //Store operation
                var asyncOperationHandle = Addressables.LoadAssetAsync<T>(value);
                asyncOperationHandlesDictionary[value] = asyncOperationHandle;

                //Handle operation
                asyncOperationHandle.Completed += OnCompleteAsyncOperationHandle;
            }
            //Share content if it was loaded
            else if (loadedAssets.Contains(value))
                LoadedAssetReferenceEvent?.Invoke(this, new LoadedAssetReferenceArgs<T>(value, asyncOperationHandlesDictionary[value]));
        }

        //Handle operation completed values
        private void OnCompleteAsyncOperationHandle(AsyncOperationHandle<T> asyncOperationHandle)
        {
            //Remove subscription
            asyncOperationHandle.Completed -= OnCompleteAsyncOperationHandle;

            //Local method, get asset reference of given operation
            AssetReference GetKey()
            {
                //Return value
                AssetReference returnValue = null;

                //Itinerate and search asset related to operation
                foreach (
                    KeyValuePair<
                        AssetReference,
                        AsyncOperationHandle<T>
                    > entry in asyncOperationHandlesDictionary
                )
                    if (entry.Value.Equals(asyncOperationHandle))
                    {
                        returnValue = entry.Key;
                        break;
                    }

                return returnValue;
            }

            //Key of operation
            AssetReference assetReference = GetKey();

            //Avoid bad loads
            if (asyncOperationHandle.IsValid())
            {
                //Remove for queuedAssets
                CollectionsTools.listTools.RemoveValue(queuedAssets, assetReference,
                new ComplexDebugInformation(this.GetType().Name, "OnCompleteAsyncOperationHandle(AsyncOperationHandle<T> asyncOperationHandle)",
                "couldnt remove assetReference from queuedAssets"));

                //Store as loaded and share
                CollectionsTools.listTools.AddValue(loadedAssets, assetReference,
                new ComplexDebugInformation(this.GetType().Name, "OnCompleteAsyncOperationHandle(AsyncOperationHandle<T> asyncOperationHandle)",
                "couldnt add assetReference to loadedAssets"));
            }
            //On operation fail, release resources and remove it
            else
            {
                ReleaseResources(assetReference);
                Debug.LogWarning("Warning: Not valid asset reference load");
            }

            //Notify completed loading
            LoadedAssetReferenceEvent?.Invoke(this, new LoadedAssetReferenceArgs<T>(assetReference, asyncOperationHandle));
        }

        //Release resources used on asset reference
        public virtual void ReleaseResources(AssetReference value)
        {
            //Release completed operation
            bool removeCompletedOperation =
                    CollectionsTools.listTools.RemoveValue(loadedAssets, value,
                    new ComplexDebugInformation(this.GetType().Name, "ReleaseResources(AssetReference value)",
                    "couldnt remove value from loadedAssets"))
                    && asyncOperationHandlesDictionary.ContainsKey(value),
                removeTriedOperation =
                    CollectionsTools.listTools.RemoveValue(queuedAssets, value,
                    new ComplexDebugInformation(this.GetType().Name, "ReleaseResources(AssetReference value)",
                    "couldnt remove value from queuedAssets"))
                    && asyncOperationHandlesDictionary.ContainsKey(value);

            //Local method, release resource and remove from dictionary
            void ReleaseOperation()
            {
                Addressables.Release(asyncOperationHandlesDictionary[value]);
                asyncOperationHandlesDictionary.Remove(value);
            }

            //Check if asset reference was loaded
            if (removeCompletedOperation)
                ReleaseOperation();
            //Check if asset reference was tried to load
            else if (removeTriedOperation)
                ReleaseOperation();
            //Notify no loaded resource
            else if (!asyncOperationHandlesDictionary.ContainsKey(value))
                Debug.LogWarning("Warning: Tried to released a not loaded asset reference");
        }

        //Release all the resources used on asset references
        public void Dispose()
        {
            //Release all the operations
            foreach (
                KeyValuePair<
                    AssetReference,
                    AsyncOperationHandle<T>
                > asyncOperationsEntry in asyncOperationHandlesDictionary
            )
            {
                //Release loaded operations
                if (asyncOperationsEntry.Value.IsDone)
                    ReleaseResources(asyncOperationsEntry.Key);
                //Release on operation complete
                else
                {
                    asyncOperationsEntry.Value.Completed -= OnCompleteAsyncOperationHandle;
                    asyncOperationsEntry.Value.Completed += (operation) => ReleaseResources(asyncOperationsEntry.Key);
                }
            }
        }

        //Default, destructor
        ~AAddressablesLoader() => Dispose();
    }
}