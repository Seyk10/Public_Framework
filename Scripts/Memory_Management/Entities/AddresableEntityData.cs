using MECS.Core;
using MECS.Tools;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MECS.MemoryManagement.Entity
{
    //* Data structure used on AddresableEntities
    public class AddresableEntityData : AData, IAddresableEntityData
    {
        //Variables
        private readonly AssetReference assetReference = null;
        public AssetReference AssetReference => assetReference;
        private readonly IResourceLoader<AssetReference> iResourceLoader = null;
        public IResourceLoader<AssetReference> IResourceLoader => iResourceLoader;
        private readonly GameObject entity = null;
        public GameObject Entity => entity;

        //Default builder
        public AddresableEntityData(AssetReference assetReference, IResourceLoader<AssetReference> iResourceLoader, GameObject entity)
        {
            this.assetReference = assetReference;
            this.iResourceLoader = iResourceLoader;
            this.entity = entity;
        }

        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityAwakeArgs<IAddresableEntityData> addresableEntityArgs = new((IAddresableEntityData)this, complexDebugInformation);

            //Notify IAddresableEntityData 
            NotifyDataPhase<EntityAwakeArgs<IAddresableEntityData>, IAddresableEntityData>(sender, addresableEntityArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityEnableArgs<IAddresableEntityData> addresableEntityArgs = new((IAddresableEntityData)this, complexDebugInformation);

            //Notify IAddresableEntityData 
            NotifyDataPhase<EntityEnableArgs<IAddresableEntityData>, IAddresableEntityData>(sender, addresableEntityArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDisableArgs<IAddresableEntityData> addresableEntityArgs = new((IAddresableEntityData)this, complexDebugInformation);

            //Notify IAddresableEntityData 
            NotifyDataPhase<EntityDisableArgs<IAddresableEntityData>, IAddresableEntityData>(sender, addresableEntityArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDestroyArgs<IAddresableEntityData> addresableEntityArgs = new((IAddresableEntityData)this, complexDebugInformation);

            //Notify IAddresableEntityData 
            NotifyDataPhase<EntityDestroyArgs<IAddresableEntityData>, IAddresableEntityData>(sender, addresableEntityArgs);
        }
    }
}