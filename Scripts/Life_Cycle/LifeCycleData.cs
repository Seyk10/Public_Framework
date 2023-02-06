using System;
using MECS.Core;
using MECS.Events;
using MECS.Tools;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.LifeCycle
{
    //* Data structure used on life cycle components
    [Serializable]
    public class LifeCycleData : AData, ILifeCycleData, IEventData
    {
        //Editor variables
        [Header("ILifeCycleData references")]
        [SerializeField] private BoolReference hasAwakeResponse = null;
        public BoolReference HasAwakeResponse => hasAwakeResponse;
        [SerializeField] private BoolReference hasEnableResponse = null;
        public BoolReference HasEnableResponse => hasEnableResponse;
        [SerializeField] private BoolReference hasStartResponse = null;
        public BoolReference HasStartResponse => hasStartResponse;
        [SerializeField] private BoolReference hasDisableResponse = null;
        public BoolReference HasDisableResponse => hasDisableResponse;
        [SerializeField] private BoolReference hasDestroyResponse = null;
        public BoolReference HasDestroyResponse => hasDestroyResponse;

        [Header("IEventData values")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;

        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityAwakeArgs<LifeCycleData> lifeCycleArgs = new((LifeCycleData)this, complexDebugInformation);
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify LifeCycleData 
            NotifyDataPhase<EntityAwakeArgs<LifeCycleData>, LifeCycleData>(sender, lifeCycleArgs);
            //Notify IEventData 
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityEnableArgs<LifeCycleData> lifeCycleArgs = new((LifeCycleData)this, complexDebugInformation);
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify LifeCycleData 
            NotifyDataPhase<EntityEnableArgs<LifeCycleData>, LifeCycleData>(sender, lifeCycleArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDisableArgs<LifeCycleData> lifeCycleArgs = new((LifeCycleData)this, complexDebugInformation);
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify LifeCycleData 
            NotifyDataPhase<EntityDisableArgs<LifeCycleData>, LifeCycleData>(sender, lifeCycleArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDestroyArgs<LifeCycleData> lifeCycleArgs = new((LifeCycleData)this, complexDebugInformation);
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify LifeCycleData 
            NotifyDataPhase<EntityDestroyArgs<LifeCycleData>, LifeCycleData>(sender, lifeCycleArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
    }
}