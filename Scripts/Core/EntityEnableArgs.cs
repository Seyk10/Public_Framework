namespace MECS.Core
{
    //* Args used to share data when entity destroy
    public class EntityEnableArgs<T> : AEntityArgs<T>
    {
        //Default builder
        public EntityEnableArgs(T data, string debugMessage) : base(data, debugMessage) { }
    }
}