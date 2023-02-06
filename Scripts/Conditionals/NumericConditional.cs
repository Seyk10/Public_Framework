using System;
using MECS.Tools;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Conditionals
{
    //* Conditional structure used on numeric operations
    //* T = NumericReference
    //* T2 = ENumericConditional
    [Serializable]
    public class NumericConditional : AConditional<NumericReference, ENumericConditional>
    {
        //Method, check if current conditional operation is correct
        public override bool IsOperationCorrect()
        {
            //Return value
            bool isOperationCorrect = false;

            //Store float references
            FloatReference floatReference_01 = Value_01.GetValue<FloatReference>(),
            floatReference_02 = Value_02.GetValue<FloatReference>();

            //Store int references
            IntReference intReference_01 = Value_01.GetValue<IntReference>(),
            intReference_02 = Value_02.GetValue<IntReference>();

            //Check value_01 values
            bool useFloatValue_01 = ReferenceTools.IsValueSafe(floatReference_01),
            useIntValue_01 = !useFloatValue_01 && ReferenceTools.IsValueSafe(intReference_01),
            canUseValue_01 = useFloatValue_01 || useIntValue_01,
            //Check value_02 values
            useFloatValue_02 = ReferenceTools.IsValueSafe(floatReference_02),
            useIntValue_02 = !useFloatValue_02 && ReferenceTools.IsValueSafe(intReference_02),
            canUseValue_02 = useFloatValue_02 || useIntValue_02,
            //Check if can check operation
            canMakeChecking = canUseValue_01 && canUseValue_02;

            //Set generic values
            if (canMakeChecking)
            {
                //Store generic values
                float value_01 = useFloatValue_01 ? floatReference_01.Value : intReference_01.Value,
                value_02 = useFloatValue_02 ? floatReference_02.Value : intReference_02.Value;

                //Switch different operations types
                switch (OperationType)
                {
                    //Check if value_01 is bigger than value_02
                    case ENumericConditional.Bigger:
                        //Check values                    
                        isOperationCorrect = value_01 > value_02;
                        break;

                    //Check if value_01 is bigger than value_02 or equal
                    case ENumericConditional.BiggerOrEqual:
                        //Check values      
                        isOperationCorrect = value_01 >= value_02;
                        break;

                    //Check if value_01 is equal to value_02
                    case ENumericConditional.Equal:
                        //Check values   
                        isOperationCorrect = value_01 == value_02;
                        break;

                    //Check if value_01 is smaller than value_02
                    case ENumericConditional.Smaller:
                        //Check values   
                        isOperationCorrect = value_01 < value_02;
                        break;

                    //Check if value_01 is smaller than value_02 or equal
                    case ENumericConditional.SmallerOrEqual:
                        //Check values   
                        isOperationCorrect = value_01 <= value_02;
                        break;
                }
            }
#if UNITY_EDITOR
            else
            {
                //Check possible errors
                if (!canUseValue_01)
                    Debug.LogWarning("Warning: Value_01 inst valid");

                if (!canUseValue_02)
                    Debug.LogWarning("Warning: Value_02 inst valid");
            }
#endif
            return isOperationCorrect;
        }
    }
}