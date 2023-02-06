using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Timers
{
    //* State used to stop timers executions without setting resting time
    public class PauseTimerState : ATimerState
    {
        //Default builder
        public PauseTimerState(MonoBehaviour monoBehaviour, ITimerData data, ComplexDebugInformation complexDebugInformation)
        : base(monoBehaviour, data, complexDebugInformation) { }

        //ATimerState, stop coroutines
        public override void RunState()
        {
            if (data.TimerExecution != null)
            {
                //Clean timer
                monoBehaviour.StopCoroutine(data.TimerExecution);
                data.TimerExecution = null;
            }
        }
    }
}