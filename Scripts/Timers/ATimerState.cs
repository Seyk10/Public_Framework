using MECS.Patrons.StateMachine;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Timers
{
    //* Base class for all timer states derived, used to filtrate on state machine
    //* T = ITimerData
    public abstract class ATimerState : AState<ITimerData>
    {
        //Variables
        protected MonoBehaviour monoBehaviour = null;

        //Default builder
        protected ATimerState(MonoBehaviour monoBehaviour, ITimerData data, ComplexDebugInformation complexDebugInformation)
        : base(data, complexDebugInformation) =>
            this.monoBehaviour = monoBehaviour;
    }
}