#if UNITY_EDITOR
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Base class used on commands related to framework folders creation
    public abstract class ACreateMECSFolderCommand
    {
        //Variables
        protected readonly ComplexDebugInformation complexDebugInformation = null;

        //Base builder
        public ACreateMECSFolderCommand(ComplexDebugInformation complexDebugInformation)
           => this.complexDebugInformation = complexDebugInformation;
    }
}
#endif