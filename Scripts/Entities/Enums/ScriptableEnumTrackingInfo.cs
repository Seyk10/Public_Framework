using MECS.Core;
using UnityEngine;

namespace MECS.Entities.Enums
{
    //* Tracking information for scriptable enums
    public class ScriptableEnumTrackingInfo : ADataTrackingInfo, IScriptableEnumData
    {
        //Editor variables
        [Header("Scriptable tracking info")]
        [SerializeReference] private ScriptableEnum scriptableEnum = null;

        //Attributes
        public ScriptableEnum ScriptableEnum => scriptableEnum;

        //Base builder
        public ScriptableEnumTrackingInfo(string entityName, ScriptableEnum scriptableEnum) : base(entityName) =>
        this.scriptableEnum = scriptableEnum;
    }
}