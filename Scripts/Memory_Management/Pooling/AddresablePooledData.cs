using System;
using MECS.Core;
using MECS.Tools;
using UnityEngine;

namespace MECS.MemoryManagement.Entity.Pooling
{
    [Serializable]
    //* Data structure for addresable pooled entities
    public class AddresablePooledData : AData, IAddresablePooledData
    {
        //Editor variables
        [SerializeField] private AddresableEntityPool addresableEntityPool = null;

        //Variables
        public bool IsPooledObject { get; set; }
        public GameObject AssociatedEntity { get; set; }
        public AddresableEntityPool AddresableEntityPool { get => addresableEntityPool; set => addresableEntityPool = value; }

        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityAwakeArgs<IAddresablePooledData> addresablePooledArgs = new((IAddresablePooledData)this, complexDebugInformation);

            //Notify IAddresablePooledData 
            NotifyDataPhase<EntityAwakeArgs<IAddresablePooledData>, IAddresablePooledData>(sender, addresablePooledArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityEnableArgs<IAddresablePooledData> addresablePooledArgs = new((IAddresablePooledData)this, complexDebugInformation);

            //Notify IAddresablePooledData 
            NotifyDataPhase<EntityEnableArgs<IAddresablePooledData>, IAddresablePooledData>(sender, addresablePooledArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDisableArgs<IAddresablePooledData> addresablePooledArgs = new((IAddresablePooledData)this, complexDebugInformation);

            //Notify IAddresablePooledData 
            NotifyDataPhase<EntityDisableArgs<IAddresablePooledData>, IAddresablePooledData>(sender, addresablePooledArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDestroyArgs<IAddresablePooledData> addresablePooledArgs = new((IAddresablePooledData)this, complexDebugInformation);

            //Notify IAddresablePooledData 
            NotifyDataPhase<EntityDestroyArgs<IAddresablePooledData>, IAddresablePooledData>(sender, addresablePooledArgs);
        }
    }
}