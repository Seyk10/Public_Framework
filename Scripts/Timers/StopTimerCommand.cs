using System;
using UnityEngine;

namespace MECS.Timers
{
    //* Command used to stop target timer execution
    [CreateAssetMenu(fileName = "New_Stop_Timer_State_Command", menuName = "MECS/Commands/States/Stop_Timer_State")]
    public class StopTimerCommand : ATimerStateCommand
    {
        //Event
        //Notify when timer stop
        //T = TimerComponent
        public static event EventHandler<TimerComponent> StopTimerEvent = null;

        //TimerStateCommand, notify command execution
        public override void Execute(TimerComponent parameter)
        {
            //Notify timer stop
            StopTimerEvent?.Invoke(this, parameter);
            base.Execute(parameter);
        }
    }
}