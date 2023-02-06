using System;
using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Conditionals
{
    //* Args used on scriptable command conditionals
    public class ScriptableConditionalArgs<T> : EventArgs, IValuesChecking
    {
        //Variables
        private readonly T value = default;
        public T Value => value;
        private readonly ConditionalComponent component = null;
        public ConditionalComponent Component => component;

        //Default builder
        public ScriptableConditionalArgs(T value, ConditionalComponent component)
        {
            this.value = value;
            this.component = component;
        }

        //IValuesChecking method, check if values on args are valid
        public bool AreValuesValid() =>
        //Check args values
        ReferenceTools.AreValuesSafe(new object[] { value, component },
        new ComplexDebugInformation(this.GetType().Name, "AreValuesValid()", "values on args aren't safe"));
    }
}