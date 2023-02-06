
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Abstract class used on MECS scriptable object command creation
    //* T = bool, indicate if could create scriptable objects
    public abstract class ACreateMECSScriptableObjectCommand
    {
        //Variables
        protected readonly ComplexDebugInformation complexDebugInformation = null;
        protected Dictionary<string, ScriptableObject> assetsDictionary = null;

        //Default builder
        protected ACreateMECSScriptableObjectCommand(ComplexDebugInformation complexDebugInformation)
        => this.complexDebugInformation = complexDebugInformation;
    }
}
#endif