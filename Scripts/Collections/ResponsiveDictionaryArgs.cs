using System;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Collections
{
    //* Args used to share dictionary values
    //* T = Key
    //* T2 =  Value
    public class ResponsiveDictionaryArgs<T, T2> : EventArgs, IValuesChecking
    {
        //Variables
        public readonly T key = default;
        public readonly T2 value = default;
        public readonly ComplexDebugInformation complexDebugInformation = null;

        //Default builder
        public ResponsiveDictionaryArgs(T key, T2 value, ComplexDebugInformation complexDebugInformation)
        {
            this.key = key;
            this.value = value;
            this.complexDebugInformation = complexDebugInformation;
        }

        //Builder without debug information
        public ResponsiveDictionaryArgs(T key, T2 value)
        {
            this.key = key;
            this.value = value;
            complexDebugInformation = new(this.GetType().Name, "ResponsiveDictionaryArgs(T key, T2 value)", "check values used");
        }

        //IValuesChecking method, check values on args
        public bool AreValuesValid() =>
            //Check values
            ReferenceTools.AreValuesSafe(new object[] { key, value },
            new ComplexDebugInformation(this.GetType().Name, "AreValuesValid()", "values on args aren't valid"));
    }
}