using System;
using MECS.Core;
using UnityEngine;

namespace MECS.Entities.Enums
{
    [Serializable]
    //* Data structure to hold enum values
    public class ScriptableEnumData : AData, IScriptableEnumData
    {
        #region SCRIPTABLE_ENUM_VALUES
        //Editor variables
        [SerializeField] private ScriptableEnum scriptableEnums = null;
        public ScriptableEnum ScriptableEnum => scriptableEnums;
        #endregion

        #region ADATA_LIFE_CYCLE_NOTIFICATION_METHODS
        //IScriptableEnumData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender)
        {
            //Args to send
            EntityAwakeArgs<IScriptableEnumData> scriptableEnumArgs = new((IScriptableEnumData)this,
            " couldnt notify IScriptableEnumData awake");

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityAwakeArgs<IScriptableEnumData>, IScriptableEnumData>(sender, scriptableEnumArgs);
        }

        //IScriptableEnumData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender)
        {
            //Args to send
            EntityDestroyArgs<IScriptableEnumData> scriptableEnumArgs = new((IScriptableEnumData)this,
            " couldnt notify IScriptableEnumData destroy");

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityDestroyArgs<IScriptableEnumData>, IScriptableEnumData>(sender, scriptableEnumArgs);
        }

        //IScriptableEnumData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender)
        {
            //Args to send
            EntityDisableArgs<IScriptableEnumData> scriptableEnumArgs = new((IScriptableEnumData)this,
            " couldnt notify IScriptableEnumData disable");

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityDisableArgs<IScriptableEnumData>, IScriptableEnumData>(sender, scriptableEnumArgs);
        }

        //IScriptableEnumData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender)
        {
            //Args to send
            EntityEnableArgs<IScriptableEnumData> scriptableEnumArgs = new((IScriptableEnumData)this,
            " couldnt notify IScriptableEnumData enable");

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityEnableArgs<IScriptableEnumData>, IScriptableEnumData>(sender, scriptableEnumArgs);
        }
        #endregion
    }
}