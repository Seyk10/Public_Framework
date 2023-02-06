namespace MECS.Variables.References
{
    //* Interface used to edit values on references ith generic types
    public interface IGenericEditableReference
    {
        //Value to get from reference
        public T3 GetValue<T3>();
        //Value to set on reference
        public bool SetValue<T3>(T3 value);
    }
}