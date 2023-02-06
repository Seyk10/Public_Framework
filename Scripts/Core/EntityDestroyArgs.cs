using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Args used to share data when entity destroy
    public class EntityDestroyArgs<T> : AEntityArgs<T>
    {
        //Default builder
        public EntityDestroyArgs(T data, ComplexDebugInformation complexDebugInformation)
        : base(data, complexDebugInformation) { }
    }
}