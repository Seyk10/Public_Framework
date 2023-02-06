using System;
using MECS.Core;
using MECS.Events;
using MECS.Tools;
using UnityEngine;

namespace MECS.GameEvents
{
    [Serializable]
    //* Data structure used on game event listener components
    public class GameEventListenerData : AData, IGameEventListenerData, IEventData
    {
        [Header("IGameEventListenerData references")]
        [SerializeField] private GameEvent gameEvent = null;
        public GameEvent GameEvent => gameEvent;

        [Header("IEventData values")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;

        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityAwakeArgs<IGameEventListenerData> gameEventListenerArgs = new((IGameEventListenerData)this, complexDebugInformation);
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityAwakeArgs<IGameEventListenerData>, IGameEventListenerData>(sender, gameEventListenerArgs);
            //Notify IEventData 
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityEnableArgs<IGameEventListenerData> gameEventListenerArgs = new((IGameEventListenerData)this, complexDebugInformation);
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityEnableArgs<IGameEventListenerData>, IGameEventListenerData>(sender, gameEventListenerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDisableArgs<IGameEventListenerData> gameEventListenerArgs = new((IGameEventListenerData)this, complexDebugInformation);
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityDisableArgs<IGameEventListenerData>, IGameEventListenerData>(sender, gameEventListenerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDestroyArgs<IGameEventListenerData> gameEventListenerArgs = new((IGameEventListenerData)this, complexDebugInformation);
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityDestroyArgs<IGameEventListenerData>, IGameEventListenerData>(sender, gameEventListenerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
    }
}