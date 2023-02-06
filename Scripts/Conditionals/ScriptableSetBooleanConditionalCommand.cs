using UnityEngine;

namespace MECS.Conditionals
{
    //* Scriptable command used to boolean values to numeric conditionals from editor
    //* T = bool
    [CreateAssetMenu(fileName = "New_Set_Boolean_Conditional_Command", menuName = "MECS/Commands/Conditionals/Set_Boolean_Conditional")]
    public class ScriptableSetBooleanConditionalCommand : AScriptableConditionalCommand<bool> { }
}