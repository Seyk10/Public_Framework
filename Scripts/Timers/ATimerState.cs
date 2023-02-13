using MECS.Patrons.StateMachine;
using UnityEngine;

namespace MECS.Timers
{
    //* Base class for all timer states derived, used to filtrate on state machine
    //* T = ITimerData
    public abstract class ATimerState : AEntityState<ITimerData>
    {
        //Default builder
        protected ATimerState(MonoBehaviour monoBehaviour, ITimerData data) : base(monoBehaviour, data) { }
    }
}