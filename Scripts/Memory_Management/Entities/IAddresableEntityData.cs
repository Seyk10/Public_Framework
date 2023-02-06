using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MECS.MemoryManagement.Entity
{
    //* Interface used on addresable entities
    public interface IAddresableEntityData
    {
        //Variables
        public AssetReference AssetReference { get; }
        public IResourceLoader<AssetReference> IResourceLoader { get; }
        public GameObject Entity { get; }
    }
}