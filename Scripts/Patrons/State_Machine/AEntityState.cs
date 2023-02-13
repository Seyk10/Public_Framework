using MECS.Tools;
using UnityEngine;

namespace MECS.Patrons.StateMachine
{
    //* Base class used on states with entities
    //* T = Interface type of data
    public abstract class AEntityState<T> : AState<T> where T : class
    {
        //Variables
        protected MonoBehaviour monoBehaviour = null;

        //AState, base builder
        protected AEntityState(MonoBehaviour monoBehaviour, T data) : base(data) =>
            this.monoBehaviour = monoBehaviour;

        //AState method, check values on state
        public override bool AreValuesValid() =>
            //Base checking
            base.AreValuesValid()

            //Check entity
            && ReferenceTools.IsValueSafe(monoBehaviour, " given monoBehaviour inst safe");
    }
}