using System;
using UnityEngine;

namespace MECS.Timers
{
    //* Command used to run target timer execution
    [CreateAssetMenu(fileName = "New_Run_Timer_State_Command", menuName = "MECS/Commands/States/Run_Timer_State")]
    public class RunTimerCommand : ATimerStateCommand
    {
        //Event
        //Notify when timer run
        //T = TimerComponent
        public static event EventHandler<TimerComponent> RunTimerEvent = null;

        //TimerStateCommand, notify command execution
        public override void Execute(TimerComponent parameter)
        {
            //Notify timer run
            RunTimerEvent?.Invoke(this, parameter);
            base.Execute(parameter);
        }
    }
}