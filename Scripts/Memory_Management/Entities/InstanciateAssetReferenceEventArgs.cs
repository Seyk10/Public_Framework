using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MECS.MemoryManagement.Entity
{
    //* Args used to share editor instantiation command values
    public class InstantiateAssetReferenceEventArgs : EventArgs
    {
        //Variables
        public readonly AssetReference assetReference = null;
        public readonly GameObjectAddressablesLoader gameObjectAddressablesLoader = null;
        public readonly Transform spawnPoint = null;

        //Default builder
        public InstantiateAssetReferenceEventArgs(AssetReference assetReference, GameObjectAddressablesLoader gameObjectAddressablesLoader, Transform spawnPoint)
        {
            this.assetReference = assetReference;
            this.gameObjectAddressablesLoader = gameObjectAddressablesLoader;
            this.spawnPoint = spawnPoint;
        }
    }
}