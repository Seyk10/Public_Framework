using System;
using MECS.Variables.References;

namespace MECS.Conditionals
{
    //* Conditional structure used on string operations
    //* T = StringReference
    //* T2 = EConditional
    [Serializable]
    public class StringConditional : AConditional<StringReference, EConditional>
    {
        //Method, check if current conditional operation is correct
        public override bool IsOperationCorrect()
        {
            //Return value
            bool isOperationCorrect = false;

            //Switch different operations types
            switch (OperationType)
            {
                //Check if values are equal
                case EConditional.Equal:
                    isOperationCorrect = Value_01.Value.Equals(Value_02.Value);
                    break;
                //Check if values are not equal
                case EConditional.NotEqual:
                    isOperationCorrect = !Value_01.Value.Equals(Value_02.Value);
                    break;
            }

            return isOperationCorrect;
        }
    }
}