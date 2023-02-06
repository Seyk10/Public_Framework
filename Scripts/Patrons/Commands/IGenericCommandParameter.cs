using System;

namespace MECS.Patrons.Commands
{
    //* Interface used on executions with concrete types and parameters
    //* T = Parameter type
    //* T2 = Generic type used on execution
    public interface IGenericCommandParameter<T>
    {
        //Notify when command execution ends
        public event EventHandler CommandFinishedEvent;
        //Execution con generic type and parameters
        public void Execute<T2>(T value);
    }
}