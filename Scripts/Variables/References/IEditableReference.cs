namespace MECS.Variables.References
{
    //* Interface used on editable references values
    public interface IEditableReference<T>
    {
        //Attribute
        public T Value { get; set; }
    }
}