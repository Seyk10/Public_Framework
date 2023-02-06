using System;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Scriptable object used to set starting casting state from editor
    [CreateAssetMenu(fileName = "New_Start_Casting_State_Command", menuName = "MECS/Commands/States/Start_Casting_State")]
    public class StartCastingStateCommand : AStateCastingCommand
    {
        //Event
        //Notify starting casting state
        public static event EventHandler<MonoBehaviour> StartCastingStateEvent = null;

        //StateCastingCommand, execute command to notify states
        public override void Execute(CastingComponent parameter)
        {
            //Notify casting state
            StartCastingStateEvent?.Invoke(this, parameter);

            //StateCastingCommand
            base.Execute(parameter);
        }
    }
}