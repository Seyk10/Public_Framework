namespace MECS.Core
{
    //* Interface used on classes that hold data structures
    public interface IDataArray<T> where T : AData
    {
        //Variables
        public T[] Data { get; set; }
    }
}