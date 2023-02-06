using System;
using UnityEngine;

namespace MECS.Conditionals
{
    //*Abstract base class for conditionals structures
    //* T = Value type
    //* T2 = Conditional enum type
    public abstract class AConditional<T, T2> : IConditionalOperation<T, T2> where T2 : Enum
    {
        //Editor variables
        //Values to check
        [SerializeField] private T value_01 = default;
        public T Value_01 => value_01;
        [SerializeField] private T value_02 = default;
        public T Value_02 => value_02;
        //Type of checking
        [SerializeField] private T2 operationType = default;
        public T2 OperationType { get => operationType; set => operationType = value; }

        //Methods, check if operation condition is true
        public abstract bool IsOperationCorrect();
    }
}