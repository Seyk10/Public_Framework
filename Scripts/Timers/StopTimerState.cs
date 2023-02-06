using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Timers
{
    //* State used to ends timer executions and set values to end state
    public class StopTimerState : ATimerState
    {
        //Default builder
        public StopTimerState(MonoBehaviour monoBehaviour, ITimerData data, ComplexDebugInformation complexDataInformation)
        : base(monoBehaviour, data, complexDataInformation) { }

        //ATimerState, execute command
        public override void RunState()
        {
            //Check if coroutine running and stop it
            if (data.TimerExecution != null)
            {
                monoBehaviour.StopCoroutine(data.TimerExecution);
                data.TimerExecution = null;
            }

            //Set current time end value
            data.CurrentTime = 0;
        }
    }
}