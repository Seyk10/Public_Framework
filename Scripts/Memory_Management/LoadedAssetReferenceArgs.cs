using System;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MECS.MemoryManagement
{
    //* Args used to share loader information
    public class LoadedAssetReferenceArgs<T> : EventArgs
    {
        //Variables
        public readonly AssetReference assetReference = null;
        public readonly AsyncOperationHandle<T> asyncOperationHandle = default;

        //Default builder
        public LoadedAssetReferenceArgs(AssetReference assetReference, AsyncOperationHandle<T> asyncOperationHandle)
        {
            this.assetReference = assetReference;
            this.asyncOperationHandle = asyncOperationHandle;
        }
    }
}