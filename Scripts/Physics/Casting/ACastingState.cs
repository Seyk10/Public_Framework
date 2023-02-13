using MECS.Patrons.StateMachine;
using MECS.Tools;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Base state class for all the casting states
    //* T = ICastingData
    public abstract class ACastingState : AEntityState<ICastingData>
    {
        //AState, base builder
        protected ACastingState(MonoBehaviour monoBehaviour, ICastingData data) : base(monoBehaviour, data) { }
    }
}