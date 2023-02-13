using System;
using MECS.Events;
using MECS.Tools;

namespace MECS.Core
{
    //* Base class used on args to share entities phases
    //* T = Interface data type
    public abstract class AEntityArgs<T> : AEventArgs, IValuesChecking
    {
        //Variables
        public readonly T data = default;

        //Default builder
        protected AEntityArgs(T data, string debugMessage) : base(debugMessage) => this.data = data;

        //IValuesChecking method, check if args values safe
        public bool AreValuesValid() => ReferenceTools.IsValueSafe(new object[] { data }, " data isn't safe");
    }
}