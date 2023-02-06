using UnityEngine;

namespace MECS.Conditionals
{
    //* Scriptable command used to add numeric values to numeric conditionals from editor
    //* T = float
    [CreateAssetMenu(fileName = "New_Add_Numeric_Conditional_Command", menuName = "MECS/Commands/Conditionals/Add_Numeric_Conditional")]
    public class ScriptableAddNumericConditionalCommand : AScriptableConditionalCommand<float> { }
}