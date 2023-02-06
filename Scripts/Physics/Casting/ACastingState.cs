using MECS.Patrons.StateMachine;
using UnityEngine;
using static MECS.Tools.DebugTools;

namespace MECS.Physics.Casting
{
    //* Base state class for all the casting states
    //* T = ICastingData
    public abstract class ACastingState : AState<ICastingData>
    {
        //Variables
        protected MonoBehaviour monoBehaviour = null;

        //AState, base builder
        protected ACastingState(MonoBehaviour monoBehaviour, ICastingData data, ComplexDebugInformation complexDebugInformation)
        : base(data, complexDebugInformation) => this.monoBehaviour = monoBehaviour;
    }
}