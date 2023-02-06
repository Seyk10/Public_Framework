using System;

namespace MECS.Conditionals
{
    //* Interface used on data structure with conditional values
    //* T = Value type on condition
    //* T2 = Enum type to check
    public interface IConditionalOperation<T, T2> where T2 : Enum
    {
        //Variables
        //Operation values
        public T Value_01 { get; }
        public T Value_02 { get; }
        public T2 OperationType { get; set; }
        //Method, check operation
        public bool IsOperationCorrect();
    }
}