using System;
using MECS.Core;
using MECS.Events;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.LifeCycle
{
    //* Data structure used on life cycle components
    [Serializable]
    public class LifeCycleData : AData, ILifeCycleData, IEventData
    {
        #region LIFE_CYCLE_VALUES
        //Editor variables
        [Header("ILifeCycleData values")]
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
        #endregion

        #region EVENT_VALUES
        [Header("IEventData values")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;
        #endregion

        #region ADATA_LIFE_CYCLE_NOTIFICATIONS_METHOD
        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender)
        {
            //Args to send
            EntityAwakeArgs<ILifeCycleData> lifeCycleArgs = new((ILifeCycleData)this, " couldnt notify ILifeCycleData awake");
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData awake");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityAwakeArgs<ILifeCycleData>, ILifeCycleData>(sender, lifeCycleArgs);
            //Notify IEventData 
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender)
        {
            //Args to send
            EntityEnableArgs<ILifeCycleData> lifeCycleArgs = new((ILifeCycleData)this, " couldnt notify ILifeCycleData enable");
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData enable");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityEnableArgs<ILifeCycleData>, ILifeCycleData>(sender, lifeCycleArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender)
        {
            //Args to send
            EntityDisableArgs<ILifeCycleData> lifeCycleArgs = new((ILifeCycleData)this, " couldnt notify ILifeCycleData disable");
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData disable");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityDisableArgs<ILifeCycleData>, ILifeCycleData>(sender, lifeCycleArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender)
        {
            //Args to send
            EntityDestroyArgs<ILifeCycleData> lifeCycleArgs = new((ILifeCycleData)this, " couldnt notify ILifeCycleData destroy");
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify ILifeCycleData destroy");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityDestroyArgs<ILifeCycleData>, ILifeCycleData>(sender, lifeCycleArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
        #endregion
    }
}