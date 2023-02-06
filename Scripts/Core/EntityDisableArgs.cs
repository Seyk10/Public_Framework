using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Args used to share data when entity disable
    public class EntityDisableArgs<T> : AEntityArgs<T>
    {
        //Default builder
        public EntityDisableArgs(T data, ComplexDebugInformation complexDebugInformation)
        : base(data, complexDebugInformation) { }
    }
}