namespace MECS.Core
{
    //* Args used to share data when entity disable
    public class EntityDisableArgs<T> : AEntityArgs<T>
    {
        //Default builder
        public EntityDisableArgs(T data, string debugMessage) : base(data, debugMessage) { }
    }
}