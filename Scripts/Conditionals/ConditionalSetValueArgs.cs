using System;

namespace MECS.Conditionals
{
    //* Base class used on commands operations with conditional
    //* T = Value to set
    //* T2 = Conditionals type
    //* T3 = Conditionals value type
    //* T4 = Enum type
    public class ConditionalSetValueArgs<T, T2, T3> : EventArgs where T : AConditional<T2, T3> where T3 : Enum
    {
        //Variables
        private readonly T[] conditionals = default;
        public T[] Conditionals => conditionals;

        //Default builder
        public ConditionalSetValueArgs(T[] conditionals) => this.conditionals = conditionals;
    }
}