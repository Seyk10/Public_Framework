using System;
using MECS.Patrons.Commands;
using MECS.Variables;
using static UnityEngine.InputSystem.InputAction;

namespace MECS.Input
{
    //* Track values from input contexts of given type
    public class TrackValueCommand<T> : ICommandParameter<CallbackContext> where T : struct
    {
        //Variables
        private readonly AVariable<T> variable = null;

        //Default builder
        public TrackValueCommand(AVariable<T> variable) => this.variable = variable;
        //Notify when command ends        
        public event EventHandler<CallbackContext> CommandFinishedEvent = null;

        //ICommandParameter, used to track input value of given type
        public void Execute(CallbackContext parameter) 
        {
            variable.Value = parameter.ReadValue<T>();
            CommandFinishedEvent?.Invoke(this, parameter);
        } 
    }
}