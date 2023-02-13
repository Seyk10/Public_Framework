using MECS.Core;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace MECS.MemoryManagement.Entity
{
    //* Data structure used on AddresableEntities
    public class AddresableEntityData : AData, IAddresableEntityData
    {
        #region ADDRESABLE_ENTITY_VALUES
        //Editor variables
        [Header("ILifeCycleData values")]
        private readonly AssetReference assetReference = null;
        public AssetReference AssetReference => assetReference;
        private readonly IResourceLoader<AssetReference> iResourceLoader = null;
        public IResourceLoader<AssetReference> IResourceLoader => iResourceLoader;
        private readonly GameObject entity = null;
        public GameObject Entity => entity;
        #endregion

        //Default builder
        public AddresableEntityData(AssetReference assetReference, IResourceLoader<AssetReference> iResourceLoader, GameObject entity)
        {
            this.assetReference = assetReference;
            this.iResourceLoader = iResourceLoader;
            this.entity = entity;
        }

        #region ADATA_LIFE_CYCLE_NOTIFICATIONS_METHODS
        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender)
        {
            //Args to send
            EntityAwakeArgs<IAddresableEntityData> addresableEntityArgs = new((IAddresableEntityData)this,
            " couldnt notify IAddresableEntityData awake");

            //Notify IAddresableEntityData 
            NotifyDataPhase<EntityAwakeArgs<IAddresableEntityData>, IAddresableEntityData>(sender, addresableEntityArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender)
        {
            //Args to send
            EntityEnableArgs<IAddresableEntityData> addresableEntityArgs = new((IAddresableEntityData)this,
            " couldnt notify IAddresableEntityData enable");

            //Notify IAddresableEntityData 
            NotifyDataPhase<EntityEnableArgs<IAddresableEntityData>, IAddresableEntityData>(sender, addresableEntityArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender)
        {
            //Args to send
            EntityDisableArgs<IAddresableEntityData> addresableEntityArgs = new((IAddresableEntityData)this,
            " couldnt notify IAddresableEntityData disable");

            //Notify IAddresableEntityData 
            NotifyDataPhase<EntityDisableArgs<IAddresableEntityData>, IAddresableEntityData>(sender, addresableEntityArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender)
        {
            //Args to send
            EntityDestroyArgs<IAddresableEntityData> addresableEntityArgs = new((IAddresableEntityData)this,
            " couldnt notify IAddresableEntityData destroy");

            //Notify IAddresableEntityData 
            NotifyDataPhase<EntityDestroyArgs<IAddresableEntityData>, IAddresableEntityData>(sender, addresableEntityArgs);
        }
        #endregion
    }
}