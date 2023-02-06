using System;
using MECS.Patrons.Commands;
using UnityEngine.InputSystem;
using static MECS.Tools.DebugTools;
using static UnityEngine.InputSystem.InputAction;

namespace MECS.Input
{
    //* Track values from button type input calls
    public class TrackButtonStatesCommand : ICommandParameter<CallbackContext>
    {
        //Variables
        private readonly ButtonInputStates buttonInputStates = null;

        //Default builder
        public TrackButtonStatesCommand(ButtonInputStates buttonInputStates) => this.buttonInputStates = buttonInputStates;

        //Notify when command finish
        public event EventHandler<CallbackContext> CommandFinishedEvent = null;

        //ICommandParameter, track values from button type input states 
        public void Execute(CallbackContext parameter)
        {
            //Debug information
            BasicDebugInformation basicDebugInformation
            = new(this.GetType().Name, "Execute(CallbackContext parameter)");

            //Check call back phases
            switch (parameter.phase)
            {
                case InputActionPhase.Performed:
                    //Switch value
                    buttonInputStates.ButtonPerformed.VariableValue.Value = true;
                    buttonInputStates.ButtonPerformed.VariableValue.Value = false;
                    buttonInputStates.ButtonPerformed.GameEvent.RaiseEvent();
                    buttonInputStates.ButtonPerformed.EventReference
                    .RaiseEvents("Input System", buttonInputStates.ButtonPerformed.VariableValue.name,
                    new ComplexDebugInformation(basicDebugInformation, "couldnt raise events"));
                    break;

                case InputActionPhase.Canceled:
                    //Switch value
                    buttonInputStates.ButtonCanceled.VariableValue.Value = true;
                    buttonInputStates.ButtonCanceled.VariableValue.Value = false;
                    buttonInputStates.ButtonCanceled.GameEvent.RaiseEvent();
                    buttonInputStates.ButtonCanceled.EventReference
                    .RaiseEvents("Input System", buttonInputStates.ButtonCanceled.VariableValue.name,
                    new ComplexDebugInformation(basicDebugInformation, "couldnt raise events"));
                    break;
            }

            CommandFinishedEvent?.Invoke(this, parameter);
        }
    }
}