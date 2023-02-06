using UnityEngine;

namespace MECS.Conditionals
{
    //* Scriptable command used to string values to numeric conditionals from editor
    //* T = string
    [CreateAssetMenu(fileName = "New_Set_String_Conditional_Command", menuName = "MECS/Commands/Conditionals/Set_String_Conditional")]
    public class ScriptableSetStringConditionalCommand : AScriptableConditionalCommand<string> { }
}