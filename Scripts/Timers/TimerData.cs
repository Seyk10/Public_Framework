using System;
using MECS.Core;
using MECS.Events;
using MECS.Tools;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Timers
{
    //* Data structure used on timer components
    [Serializable]
    public class TimerData : AData, ITimerData, IEventData
    {
        //Editor variables
        [Header("ITimerData values")]
        [SerializeField] private FloatReference time = null;
        public FloatReference Time => time;

        //Variables
        public float CurrentTime { get; set; }
        public Coroutine TimerExecution { get; set; }
        private TimerStateMachine stateMachine = new();
        public TimerStateMachine StateMachine => stateMachine;
        public bool IsTimerInitialized { get; set; }

        [Header("IEventData values")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;

        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityAwakeArgs<ITimerData> timerArgs = new((ITimerData)this, complexDebugInformation);
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify ITimerData 
            NotifyDataPhase<EntityAwakeArgs<ITimerData>, ITimerData>(sender, timerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityEnableArgs<ITimerData> timerArgs = new((ITimerData)this, complexDebugInformation);
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify ITimerData 
            NotifyDataPhase<EntityEnableArgs<ITimerData>, ITimerData>(sender, timerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDisableArgs<ITimerData> timerArgs = new((ITimerData)this, complexDebugInformation);
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify ITimerData 
            NotifyDataPhase<EntityDisableArgs<ITimerData>, ITimerData>(sender, timerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDestroyArgs<ITimerData> timerArgs = new((ITimerData)this, complexDebugInformation);
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify ITimerData 
            NotifyDataPhase<EntityDestroyArgs<ITimerData>, ITimerData>(sender, timerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
    }
}