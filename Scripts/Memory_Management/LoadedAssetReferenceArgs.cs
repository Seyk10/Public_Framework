using MECS.Events;
using MECS.Tools;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MECS.MemoryManagement
{
    //* Args used to share loader information
    public class LoadedAssetReferenceArgs<T> : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly AssetReference assetReference = null;
        public readonly AsyncOperationHandle<T> asyncOperationHandle = default;

        //Default builder
        public LoadedAssetReferenceArgs(AssetReference assetReference, AsyncOperationHandle<T> asyncOperationHandle, string debugMessage)
        : base(debugMessage)
        {
            this.assetReference = assetReference;
            this.asyncOperationHandle = asyncOperationHandle;
        }

        //IValuesChecking method, check args values
        public bool AreValuesValid() =>
            //Check asset reference
            ReferenceTools.IsValueSafe(assetReference, " given assetReference isn't safe")

            //Check operation reference
            && ReferenceTools.IsValueSafe(asyncOperationHandle, " given asyncOperationHandle isn't safe");
    }
}