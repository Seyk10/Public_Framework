using System;

namespace MECS.Patrons.StateMachine
{
    //* Args used to share target state type and target data to set state
    //* T = Data interface type
    public class StateArgs<T> : EventArgs where T : class
    {
        //Variables
        //Sender of event
        public readonly object sender = null;
        //State to set
        public readonly AState<T> state = null;
        //Target data
        public readonly T data = null;

        //Default builder, store sender, target component and state to set
        public StateArgs(object sender, AState<T> state, T data)
        {
            //Set values
            this.sender = sender;
            this.state = state;
            this.data = data;
        }
    }
}