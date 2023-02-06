using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Physics.Casting
{
    //* Casting state used to pause detections
    public class SleepingState : ACastingState
    {
        //ACastingState, default builder
        public SleepingState(MonoBehaviour monoBehaviour, ICastingData data, ComplexDebugInformation complexDebugInformation)
        : base(monoBehaviour, data, complexDebugInformation) { }

        //ACastingState, stop casting process 
        public override void RunState() => monoBehaviour.StopCoroutine(data.CastingExecution);
    }
}