using System;
using UnityEngine;

namespace MECS.Timers
{
    //* Command used to pause target timer execution
    [CreateAssetMenu(fileName = "New_Pause_Timer_State_Command", menuName = "MECS/Commands/States/Pause_Timer_State")]
    public class PauseTimerCommand : ATimerStateCommand
    {
        //Event
        //Notify when timer initialize
        //T = TimerComponent
        public static event EventHandler<TimerComponent> PauseTimerEvent = null;

        //TimerStateCommand, notify command execution
        public override void Execute(TimerComponent parameter)
        {
            //Notify timer pause
            PauseTimerEvent?.Invoke(this, parameter);
            base.Execute(parameter);
        }
    }
}