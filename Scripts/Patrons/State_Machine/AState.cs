using MECS.Tools;
using static MECS.Tools.DebugTools;

namespace MECS.Patrons.StateMachine
{
    //* Base class used on states
    //* T = Interface type of data
    public abstract class AState<T> : IValuesChecking, IState where T : class
    {
        //Variables
        protected readonly T data = null;
        protected readonly ComplexDebugInformation complexDebugInformation = null;

        //Default builder, save component reference of state
        public AState(T data, ComplexDebugInformation complexDebugInformation)
        {
            this.data = data;
            this.complexDebugInformation = complexDebugInformation;
        }

        //IValuesChecking method, check if given values on state are valid
        public bool AreValuesValid()
        {
            //Basic debug information
            BasicDebugInformation basicDebugInformation = new(this.GetType().Name, "AreValuesValid()");

            return
            //Check data value
            ReferenceTools.IsValueSafe(data,
            new ComplexDebugInformation(basicDebugInformation, "given data isn't valid"))

            //Check complex debug information
            && ReferenceTools.IsValueSafe(complexDebugInformation,
            new ComplexDebugInformation(basicDebugInformation, "given complexDebugInformation isn't valid"));
        }

        //Methods
        //Set execution on variable and run coroutine
        public abstract void RunState();
    }
}