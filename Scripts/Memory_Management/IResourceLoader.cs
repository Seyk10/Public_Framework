namespace MECS.MemoryManagement
{
    //* Interface used on structure which load and manage resources from memory
    public interface IResourceLoader<T>
    {
        //Methods
        //Load resources on memory
        public void LoadResource(T value);

        //Release resources from memory
        public void ReleaseResources(T value);

        //Variables
        public T[] LoadedResources { get; }
        public T[] QueuedResources { get; }
    }
}
