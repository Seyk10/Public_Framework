using MECS.Tools;

namespace MECS.Patrons.StateMachine
{
    //* Base class used on states
    //* T = Interface type of data
    public abstract class AState<T> : IValuesChecking, IState where T : class
    {
        //Variables
        protected readonly T data = null;

        //Default builder, save component reference of state
        public AState(T data) =>
            this.data = data;

        //IValuesChecking method, check if given values on state are valid
        public virtual bool AreValuesValid() =>
            //Check data value
            ReferenceTools.IsValueSafe(data, " given data isn't valid");

        //Methods
        //Set execution on variable and run coroutine
        public abstract void RunState();
    }
}