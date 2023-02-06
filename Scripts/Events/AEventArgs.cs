using System;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Events
{
    //* Abstract class used on args with complex debug information to unify a standard
    public class AEventArgs : EventArgs, IValuesChecking
    {
        //Variables
        public readonly ComplexDebugInformation complexDebugInformation = null;

        //Default builder
        public AEventArgs(ComplexDebugInformation complexDebugInformation) => this.complexDebugInformation = complexDebugInformation;

        //IValuesChecking, check if values given on args
        public virtual bool AreValuesValid() =>
            //Check given debug information
            ReferenceTools.IsValueSafe(complexDebugInformation,
            new ComplexDebugInformation(this.GetType().Name, "AreValuesValid()", "given complexDebugInformation isn't valid"));
    }
}