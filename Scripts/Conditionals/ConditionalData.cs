using System;
using MECS.Core;
using MECS.Events;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Conditionals
{
    //* Conditional data structure
    [Serializable]
    public class ConditionalData : AData, IConditionalData, IEventData
    {
        #region CONDITIONAL_VALUES
        //Editor variables  
        [Header("IConditionalData values")]
        [SerializeField] private NumericConditional[] numericConditionals = null;
        public NumericConditional[] NumericConditionals => numericConditionals;
        [SerializeField] private StringConditional[] stringConditionals = null;
        public StringConditional[] StringConditionals => stringConditionals;
        [SerializeField] private BoolConditional[] boolConditionals = null;
        public BoolConditional[] BoolConditionals => boolConditionals;
        [SerializeField] private BoolReference useNumericConditionals = null;
        public BoolReference UseNumericConditionals => useNumericConditionals;
        [SerializeField] private BoolReference useStringConditionals = null;
        public BoolReference UseStringConditionals => useStringConditionals;
        [SerializeField] private BoolReference useBoolConditionals = null;
        public BoolReference UseBoolConditionals => useBoolConditionals;
        #endregion

        #region EVENT_VALUES
        [Header("IEventData values")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;
        #endregion

        #region ADATA_LIFE_CYCLE_NOTIFICATION_METHODS
        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender)
        {
            //Args to send
            EntityAwakeArgs<IConditionalData> conditionalArgs = new((IConditionalData)this, " couldnt notify IConditionalData awake");
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData awake");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityAwakeArgs<IConditionalData>, IConditionalData>(sender, conditionalArgs);
            //Notify IEventData
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender)
        {
            //Args to send
            EntityEnableArgs<IConditionalData> conditionalArgs = new((IConditionalData)this, " couldnt notify IConditionalData enable");
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData enable");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityEnableArgs<IConditionalData>, IConditionalData>(sender, conditionalArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender)
        {
            //Args to send
            EntityDisableArgs<IConditionalData> conditionalArgs = new((IConditionalData)this,
            " couldnt notify IConditionalData disable");
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData disable");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityDisableArgs<IConditionalData>, IConditionalData>(sender, conditionalArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender)
        {
            //Args to send
            EntityDestroyArgs<IConditionalData> conditionalArgs = new((IConditionalData)this,
            " couldnt notify IConditionalData destroy");
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData destroy");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityDestroyArgs<IConditionalData>, IConditionalData>(sender, conditionalArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
        #endregion
    }
}