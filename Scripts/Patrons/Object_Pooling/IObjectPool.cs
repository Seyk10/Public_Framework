namespace MECS.Patrons.ObjectPooling
{
    //* Interface used to set object pooling functionalities 
    public interface IObjectPool<T> where T : class
    {
        //Variables
        //Return current inactive objects
        public int CountInactive { get; }
        //Method, clear pool content
        public void Clear();
        //Return pooled object of given type
        T Get();
        //Create a pooled object from a stack value
        public PooledObject<T> Get(out T value);
        //Return or destroy the pooled object to the pool
        public void ReturnObject(T element);
    }
}