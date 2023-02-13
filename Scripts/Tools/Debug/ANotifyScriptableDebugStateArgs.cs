using System;
using MECS.Events;

namespace MECS.Tools
{
    //* Base class for scriptable debug state notification commands
    public abstract class ANotifyScriptableDebugStateArgs : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly Type scriptableType = null;
        public readonly string scriptableName = null;

        //Base builder
        protected ANotifyScriptableDebugStateArgs(Type scriptableType, string scriptableName, string debugMessage) : base(debugMessage)
        {
            this.scriptableType = scriptableType;
            this.scriptableName = scriptableName;
        }

        //IValuesChecking method, check args values
        public bool AreValuesValid() =>
            //Check scriptable type
            ReferenceTools.IsValueSafe(scriptableType, " scriptableType isn't safe")

            //Check scriptable name
            && ReferenceTools.IsValueSafe(scriptableName, " scriptableName isn't safe");
    }
}