using MECS.Core;
using MECS.Tools;
using UnityEngine;

namespace MECS.Entities.Enums
{
    //* System used to check values
    //* T = IScriptableEnumData
    [CreateAssetMenu(fileName = "New_Scriptable_Enum_System", menuName = "MECS/Systems/Scriptable_Enum")]
    public class ScriptableEnumSystem : ASystem<IScriptableEnumData>
    {
        //ASystem, check if data values are valid
        protected override bool IsValidData(Component entity, IScriptableEnumData data) =>
            ReferenceTools.IsValueSafe(data.ScriptableEnum, "given scriptable enum isn't safe on: " + entity.gameObject.name);
    }
}