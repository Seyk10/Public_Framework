using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Args used to share data when entity destroy
    public class EntityEnableArgs<T> : AEntityArgs<T>
    {
        //Default builder
        public EntityEnableArgs(T data, ComplexDebugInformation complexDebugInformation)
        : base(data, complexDebugInformation) { }
    }
}