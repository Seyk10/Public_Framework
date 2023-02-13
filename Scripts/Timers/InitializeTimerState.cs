using UnityEngine;

namespace MECS.Timers
{
    //* State used to set default timer values on data
    public class InitializeTimerState : ATimerState
    {
        //Default builder
        public InitializeTimerState(MonoBehaviour monoBehaviour, ITimerData data)
        : base(monoBehaviour, data) { }

        //ATimerState, set values of timer to default 
        public override void RunState()
        {
            //Check coroutine and clean it
            if (data.TimerExecution != null)
            {
                monoBehaviour.StopCoroutine(data.TimerExecution);
                data.TimerExecution = null;
            }

            //Set time to default
            data.CurrentTime = data.Time.Value;
        }
    }
}