using MECS.Core;
using UnityEngine;

namespace MECS.Entities.Enums
{
    //* Manager used to register enum data values
    //* T = IScriptableEnumData
    [CreateAssetMenu(fileName = "New_Scriptable_Enum_Manager", menuName = "MECS/Managers/Scriptable_Enum")]
    public class ScriptableEnumManager : AManager<IScriptableEnumData> { }
}