using System;

namespace MECS.Variables
{
    //* Interface used on structures that must notify when any variable changes
    public interface IVariableChange<T>
    {
        //Event to raise
        public event EventHandler<T> VariableChangeEvent;
    }
}