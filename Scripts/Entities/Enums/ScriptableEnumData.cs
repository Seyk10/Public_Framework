using System;
using MECS.Core;
using MECS.Tools;
using UnityEngine;

namespace MECS.Entities.Enums
{
    [Serializable]
    //* Data structure to hold enum values
    public class ScriptableEnumData : AData, IScriptableEnumData
    {
        //Editor variables
        [SerializeField] private ScriptableEnum scriptableEnums = null;
        public ScriptableEnum ScriptableEnum => scriptableEnums;

        //IScriptableEnumData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityAwakeArgs<IScriptableEnumData> scriptableEnumArgs = new((IScriptableEnumData)this, complexDebugInformation);

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityAwakeArgs<IScriptableEnumData>, IScriptableEnumData>(sender, scriptableEnumArgs);
        }

        //IScriptableEnumData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDestroyArgs<IScriptableEnumData> scriptableEnumArgs = new((IScriptableEnumData)this, complexDebugInformation);

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityDestroyArgs<IScriptableEnumData>, IScriptableEnumData>(sender, scriptableEnumArgs);
        }

        //IScriptableEnumData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDisableArgs<IScriptableEnumData> scriptableEnumArgs = new((IScriptableEnumData)this, complexDebugInformation);

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityDisableArgs<IScriptableEnumData>, IScriptableEnumData>(sender, scriptableEnumArgs);
        }

        //IScriptableEnumData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityEnableArgs<IScriptableEnumData> scriptableEnumArgs = new((IScriptableEnumData)this, complexDebugInformation);

            //Notify IGameEventListenerData 
            NotifyDataPhase<EntityEnableArgs<IScriptableEnumData>, IScriptableEnumData>(sender, scriptableEnumArgs);
        }
    }
}