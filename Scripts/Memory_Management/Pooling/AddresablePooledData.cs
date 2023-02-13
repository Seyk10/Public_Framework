using System;
using MECS.Core;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    [Serializable]
    //* Data structure for addresable pooled entities
    public class AddresablePooledData : AData, IAddresablePooledData
    {
        #region ADDRESABLE_POOLED_VALUES
        //Editor variables
        [SerializeField] private AddresableEntityPool addresableEntityPool = null;

        //Variables
        public bool IsPooledObject { get; set; }
        public GameObject AssociatedEntity { get; set; }
        public AddresableEntityPool AddresableEntityPool { get => addresableEntityPool; set => addresableEntityPool = value; }
        #endregion

        #region ADATA_LIFE_CYCLE_NOTIFICATION_METHODS
        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender)
        {
            //Args to send
            EntityAwakeArgs<IAddresablePooledData> addresablePooledArgs = new((IAddresablePooledData)this,
            " couldnt notify IAddresablePooledData awake");

            //Notify IAddresablePooledData 
            NotifyDataPhase<EntityAwakeArgs<IAddresablePooledData>, IAddresablePooledData>(sender, addresablePooledArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender)
        {
            //Args to send
            EntityEnableArgs<IAddresablePooledData> addresablePooledArgs = new((IAddresablePooledData)this,
            " couldnt notify IAddresablePooledData enable");

            //Notify IAddresablePooledData 
            NotifyDataPhase<EntityEnableArgs<IAddresablePooledData>, IAddresablePooledData>(sender, addresablePooledArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender)
        {
            //Args to send
            EntityDisableArgs<IAddresablePooledData> addresablePooledArgs = new((IAddresablePooledData)this,
            " couldnt notify IAddresablePooledData disable");

            //Notify IAddresablePooledData 
            NotifyDataPhase<EntityDisableArgs<IAddresablePooledData>, IAddresablePooledData>(sender, addresablePooledArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender)
        {
            //Args to send
            EntityDestroyArgs<IAddresablePooledData> addresablePooledArgs = new((IAddresablePooledData)this,
            " couldnt notify IAddresablePooledData destroy");

            //Notify IAddresablePooledData 
            NotifyDataPhase<EntityDestroyArgs<IAddresablePooledData>, IAddresablePooledData>(sender, addresablePooledArgs);
        }
        #endregion
    }
}