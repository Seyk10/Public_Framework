namespace MECS.Core
{
    //* Args used to share data when entity destroy
    public class EntityDestroyArgs<T> : AEntityArgs<T>
    {
        //Default builder
        public EntityDestroyArgs(T data, string debugMessage) : base(data, debugMessage) { }
    }
}