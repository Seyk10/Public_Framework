using MECS.Collections;
using MECS.Events;
using MECS.Tools;

namespace MECS.Conditionals
{
    //* Args used on scriptable command conditionals
    public class ScriptableConditionalArgs<T> : AEventArgs, IValuesChecking
    {
        //Variables
        private readonly T value = default;
        public T Value => value;
        private readonly ConditionalComponent component = null;
        public ConditionalComponent Component => component;

        //Default builder
        public ScriptableConditionalArgs(T value, ConditionalComponent component, string debugMessage) : base(debugMessage)
        {
            this.value = value;
            this.component = component;
        }

        //IValuesChecking method, check if values on args are valid
        public bool AreValuesValid() =>
            //Check args values
            CollectionsTools.arrayTools.IsArrayContentSafe(new object[] { value, component },
            debugMessage + " values on args aren't safe");
    }
}