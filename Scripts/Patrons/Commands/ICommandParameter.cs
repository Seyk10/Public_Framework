using System;

namespace MECS.Patrons.Commands
{
    //* Interface used for commands executions with parameters
    //* T = Parameter type
    public interface ICommandParameter<T>
    {
        //Notify when command execution ends
        public event EventHandler<T> CommandFinishedEvent;
        //Execute command content
        public void Execute(T parameter);
    }
}