using System;

namespace MECS.Tools
{
    //* Args used to notify when a scriptable is activated
    public class NotifyScriptableDebugOnEnableArgs : ANotifyScriptableDebugStateArgs
    {
        public NotifyScriptableDebugOnEnableArgs(Type scriptableType, string scriptableName, string debugMessage)
        : base(scriptableType, scriptableName, debugMessage) { }
    }
}