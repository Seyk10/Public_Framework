using System;

namespace MECS.Patrons.Commands
{
    //* Interface used on command type executions
    public interface ICommand
    {
        //Notify when command execution ends
        public event EventHandler CommandFinishedEvent;
        //Execute command content
        public void Execute();
    }
}