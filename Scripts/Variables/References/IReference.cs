namespace MECS.Variables.References
{
    //* Interface used to set reference structures
    public interface IReference<T>
    {
        //Variables
        public T Value { get; }
    }
}