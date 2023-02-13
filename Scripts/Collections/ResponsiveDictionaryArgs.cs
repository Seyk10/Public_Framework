using MECS.Events;
using MECS.Tools;

namespace MECS.Collections
{
    //* Args used to share dictionary values
    //* T = Key
    //* T2 =  Value
    public class ResponsiveDictionaryArgs<T, T2> : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly T key = default;
        public readonly T2 value = default;

        //Default builder
        public ResponsiveDictionaryArgs(T key, T2 value, string debugMessage) : base(debugMessage)
        {
            this.key = key;
            this.value = value;
        }

        //IValuesChecking method, check values on args
        public bool AreValuesValid() =>
            //Check key
            ReferenceTools.IsValueSafe(key, " given key isn't valid")

            //Check value
            && ReferenceTools.IsValueSafe(value, " given value isn't valid");
    }
}