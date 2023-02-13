using UnityEngine;

namespace MECS.Timers
{
    //* State used to stop timers executions without setting resting time
    public class PauseTimerState : ATimerState
    {
        //Default builder
        public PauseTimerState(MonoBehaviour monoBehaviour, ITimerData data) : base(monoBehaviour, data) { }

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