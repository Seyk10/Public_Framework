using System;
using UnityEngine;

namespace MECS.Timers
{
    //* Command used to set initialize target timer values
    [CreateAssetMenu(fileName = "New_Initialize_Timer_State_Command", menuName = "MECS/Commands/States/Initialize_Timer_State")]
    public class InitializeTimerCommand : ATimerStateCommand
    {
        //Event
        //Notify when timer initialize
        //T = TimerComponent
        public static event EventHandler<TimerComponent> InitializeTimerEvent = null;

        //TimerStateCommand, notify command execution
        public override void Execute(TimerComponent parameter)
        {
            //Notify timer initialization
            InitializeTimerEvent?.Invoke(this, parameter);
            base.Execute(parameter);
        }
    }
}