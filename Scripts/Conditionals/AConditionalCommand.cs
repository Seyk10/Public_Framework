using System;
using MECS.Patrons.Commands;

namespace MECS.Conditionals
{
    //* Base class used on commands operations with conditional
    //* T = Value to set
    //* T2 = Conditionals type
    //* T3 = Conditionals value type
    //* T4 = Enum type
    public abstract class AConditionalCommand<T, T2, T3, T4> : ICommand where T2 : AConditional<T3, T4> where T4 : Enum
    {
        //Variables
        protected readonly T value = default;
        protected readonly T2[] conditionals = default;

        //Event, notify command was executed and share values
        public static event EventHandler<ConditionalSetValueArgs<T2, T3, T4>> SetValueEvent = null;
        //Notify command ends
        public event EventHandler CommandFinishedEvent = null;

        //Default builder
        protected AConditionalCommand(T value, T2[] conditionals)
        {
            //Set values
            this.value = value;
            this.conditionals = conditionals;
        }

        //Execute the logic to work with the conditional values
        public virtual void Execute()
        {
            SetValueEvent?.Invoke(this, new ConditionalSetValueArgs<T2, T3, T4>(conditionals));
            CommandFinishedEvent?.Invoke(this, null);
        }
    }
}