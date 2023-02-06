using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Args used to share data when entity awake
    //* T = interface data type
    public class EntityAwakeArgs<T> : AEntityArgs<T>
    {
        //Default builder
        public EntityAwakeArgs(T data, ComplexDebugInformation complexDebugInformation)
        : base(data, complexDebugInformation) { }
    }
}