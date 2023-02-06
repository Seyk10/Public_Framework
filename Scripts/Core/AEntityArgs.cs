using System;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Core
{
    //* Base class used on args to share entities phases
    //* T = Interface data type
    public abstract class AEntityArgs<T> : EventArgs, IValuesChecking
    {
        //Variables
        public readonly T data = default;
        public readonly ComplexDebugInformation complexDebugInformation = null;

        //Default builder
        public AEntityArgs(T data, ComplexDebugInformation complexDebugInformation)
        {
            this.data = data;
            this.complexDebugInformation = complexDebugInformation;
        }

        //IValuesChecking method, check if args values safe
        public bool AreValuesValid() =>
        //check all values on args
        ReferenceTools.AreValuesSafe(new object[] { data, complexDebugInformation },
        new ComplexDebugInformation(this.GetType().Name, "AreValuesValid()", "values on args aren't safe"));
    }
}