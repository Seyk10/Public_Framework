using System;
using MECS.Core;
using MECS.Tools;
using UnityEngine;

namespace MECS.Events
{
    [Serializable]
    //* Data structure used on event related components
    public class EventData : AData, IEventData
    {
        //Editor variables
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;

        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IEventData
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
    }
}