using System;

namespace MECS.Patrons.Commands
{
    //* Interface used to create command with a return value
    //* T = return type
    public interface ICommandReturn<T>
    {
        //Notify when command execution ends
        public event EventHandler<T> CommandFinishedEvent;
        //Execute command content and return result
        public T Execute();   
    }
}