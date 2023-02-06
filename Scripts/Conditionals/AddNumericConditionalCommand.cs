using MECS.Variables.References;
using UnityEngine;

namespace MECS.Conditionals
{
    //* Command used to add value to conditional data structure numeric conditionals
    //* T = float
    //* T2 = NumericConditional
    //* T3 = NumericReference
    //* T4 = ENumericConditional
    public class AddNumericConditionalCommand : AConditionalCommand<float, NumericConditional, NumericReference, ENumericConditional>
    {
        //Default builder
        public AddNumericConditionalCommand(float value, NumericConditional[] conditionals) : base(value, conditionals) { }

        //Add value to numeric conditionals
        public override void Execute()
        {
#if UNITY_EDITOR
            Debug.LogWarning("Warning: Functionality to implement.");
#endif
            // //Itinerate conditionals and set values
            // foreach (NumericConditional conditional in conditionals)
            // {
            //     //Check types
            //     if (conditional.Value_01.GetValue<IntReference>(out IntReference tempIntValue))
            //         //Set value to value_01
            //         tempIntValue.Value = (int)value + tempIntValue.Value;
            //     else if (conditional.Value_01.GetValue<FloatReference>(out FloatReference tempFloatValue))
            //         //Set value to value_01
            //         tempFloatValue.Value = value + tempFloatValue.Value;
            // }

            //raise event
            base.Execute();
        }
    }
}