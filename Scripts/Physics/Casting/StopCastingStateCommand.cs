using System;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Scriptable object used to set stopping casting state from editor
    [CreateAssetMenu(fileName = "New_Stop_Casting_State_Command", menuName = "MECS/Commands/States/Stop_Casting_State")]
    public class StopCastingStateCommand : AStateCastingCommand
    {
        //Event
        //Notify starting casting state
        public static event EventHandler<MonoBehaviour> StopCastingStateEvent = null;

        //StateCastingCommand, execute command to notify states
        public override void Execute(CastingComponent parameter)
        {
            //Notify casting state
            StopCastingStateEvent?.Invoke(this, parameter);

            //StateCastingCommand
            base.Execute(parameter);
        }
    }
}