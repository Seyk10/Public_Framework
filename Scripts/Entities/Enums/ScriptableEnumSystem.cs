using MECS.Core;
using MECS.Tools;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Entities.Enums
{
    //* System used to check values
    //* T = IScriptableEnumData
    [CreateAssetMenu(fileName = "New_Scriptable_Enum_System", menuName = "MECS/Systems/Scriptable_Enum")]
    public class ScriptableEnumSystem : ASystem<IScriptableEnumData>
    {
        //ASystem, check if data values are valid
        protected override bool IsValidData(Component entity, IScriptableEnumData data,
        ComplexDebugInformation complexDebugInformation)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation =
            new("ScriptableEnumSystem", "IsValidData(Component entity, IScriptableEnumData data)");
            string entityName = entity.gameObject.name;

            //Store result
            bool areValid = ReferenceTools.IsValueSafe(data.ScriptableEnum,
            complexDebugInformation.AddTempCustomText("given scriptable enum isn't safe on: " + entityName));

            return areValid;
        }
    }
}