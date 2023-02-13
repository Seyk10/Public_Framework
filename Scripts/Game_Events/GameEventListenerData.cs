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
        #region GAME_EVENT_LISTENER_VALUES
        [Header("IGameEventListenerData references")]
        [SerializeField] private GameEvent gameEvent = null;
        public GameEvent GameEvent => gameEvent;
        #endregion

        #region EVENT_VALUES
        [Header("IEventData values")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;
        #endregion

        #region ADATA_LIFE_CYCLE_NOTIFICATIONS_METHODS
        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender)
        {
            //Args to send
            EntityAwakeArgs<IGameEventListenerData> gameEventListenerArgs = new((IGameEventListenerData)this,
            " couldnt notify IGameEventListenerData awake");
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData awake");

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityAwakeArgs<IGameEventListenerData>, IGameEventListenerData>(sender, gameEventListenerArgs);
            //Notify IEventData 
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender)
        {
            //Args to send
            EntityEnableArgs<IGameEventListenerData> gameEventListenerArgs = new((IGameEventListenerData)this,
            " couldnt notify IGameEventListenerData enable");
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData enable");

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityEnableArgs<IGameEventListenerData>, IGameEventListenerData>(sender, gameEventListenerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender)
        {
            //Args to send
            EntityDisableArgs<IGameEventListenerData> gameEventListenerArgs = new((IGameEventListenerData)this,
            " couldnt notify IGameEventListenerData disable");
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData disable");

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityDisableArgs<IGameEventListenerData>, IGameEventListenerData>(sender, gameEventListenerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender)
        {
            //Args to send
            EntityDestroyArgs<IGameEventListenerData> gameEventListenerArgs = new((IGameEventListenerData)this,
            " couldnt notify IGameEventListenerData destroy");
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData destroy");

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityDestroyArgs<IGameEventListenerData>, IGameEventListenerData>(sender, gameEventListenerArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
        #endregion
    }
}