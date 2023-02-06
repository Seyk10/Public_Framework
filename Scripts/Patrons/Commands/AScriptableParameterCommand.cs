using System;
using UnityEngine;

namespace MECS.Patrons.Commands
{
    //* Base class for scriptable command with parameters
    //* T = Parameter to receive
    public abstract class AScriptableParameterCommand<T> : ScriptableObject, ICommandParameter<T>
    {
        //ICommandParameter, notify command has finish
        public event EventHandler<T> CommandFinishedEvent = null;

        //ICommandParameter, execute command content
        public virtual void Execute(T parameter) => CommandFinishedEvent?.Invoke(this, parameter);
    }
}