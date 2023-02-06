using System;
using System.Collections;
using MECS.Patrons.Commands;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Timers
{
    //* State to make timer run until the time ends, then raise the event
    public class RunTimerState : ATimerState
    {
        //Events, notify when timer ends
        public event EventHandler<ITimerData> TimerEndsEvent = null;

        //Default builder
        public RunTimerState(MonoBehaviour monoBehaviour, ITimerData data, ComplexDebugInformation complexDebugInformation)
        : base(monoBehaviour, data, complexDebugInformation) { }

        //ATimerState, start coroutine
        public override void RunState()
        {
            //Local method, loop timer
            IEnumerator TimerLoop()
            {
                //Loop timer until time ends
                do
                {
                    //Time to wait before rest values
                    float waitTimer = data.CurrentTime - 1 >= 0 ? 1 : data.CurrentTime;
                    yield return new WaitForSeconds(waitTimer);
                    //Rest time
                    data.CurrentTime -= waitTimer;

                } while (data.CurrentTime > 0);

                //Notify timer ends
                TimerEndsEvent?.Invoke(monoBehaviour, data);
            }

            data.TimerExecution = monoBehaviour.StartCoroutine(TimerLoop());
        }
    }
}