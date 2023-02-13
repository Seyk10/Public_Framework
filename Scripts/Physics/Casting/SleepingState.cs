using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Casting state used to pause detections
    public class SleepingState : ACastingState
    {
        //ACastingState, default builder
        public SleepingState(MonoBehaviour monoBehaviour, ICastingData data) : base(monoBehaviour, data) { }

        //ACastingState, stop casting process 
        public override void RunState() => monoBehaviour.StopCoroutine(data.CastingExecution);
    }
}