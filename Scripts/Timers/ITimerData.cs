using MECS.Variables.References;
using UnityEngine;

namespace MECS.Timers
{
    //* Interface used to set the variables used on timer data type structures
    public interface ITimerData
    {
        //Variables
        public TimerStateMachine StateMachine { get; }
        public FloatReference Time { get; }
        public float CurrentTime { get; set; }
        public Coroutine TimerExecution { get; set; }
        public bool IsTimerInitialized { get; set; }
    }
}