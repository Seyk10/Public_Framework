using System;
using static MECS.Tools.DebugTools;

namespace MECS.Tools
{
    //* Base class for scriptable debug state notification commands
    public abstract class ANotifyScriptableDebugStateArgs : EventArgs, IValuesChecking
    {
        //Variables
        public readonly Type scriptableType = null;
        public readonly string scriptableName = null;

        //Base builder
        protected ANotifyScriptableDebugStateArgs(Type scriptableType, string scriptableName)
        {
            this.scriptableType = scriptableType;
            this.scriptableName = scriptableName;
        }

        //IValuesChecking method, check args values
        public bool AreValuesValid() =>
        //Check debug information
        ReferenceTools.AreValuesSafe(new object[] { scriptableType, scriptableName },
        new ComplexDebugInformation(this.GetType().Name,
        "AreValuesValid(ComplexDebugInformation complexDebugInformation)", "given parameters aren't safe"));
    }
}