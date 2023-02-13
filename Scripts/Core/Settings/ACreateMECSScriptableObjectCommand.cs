
#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;

namespace MECS.Core
{
    //* Abstract class used on MECS scriptable object command creation
    //* T = bool, indicate if could create scriptable objects
    public abstract class ACreateMECSScriptableObjectCommand
    {
        //Variables
        protected Dictionary<string, ScriptableObject> assetsDictionary = null;
    }
}
#endif