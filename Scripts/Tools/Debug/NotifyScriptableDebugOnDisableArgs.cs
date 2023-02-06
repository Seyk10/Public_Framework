using System;

namespace MECS.Tools
{
    //* Args used to notify when a scriptable is activated
    public class NotifyScriptableDebugOnDisableArgs : ANotifyScriptableDebugStateArgs
    {
        //Base builder
        public NotifyScriptableDebugOnDisableArgs(Type scriptableType, string scriptableName) : base(scriptableType, scriptableName)
        { }
    }
}