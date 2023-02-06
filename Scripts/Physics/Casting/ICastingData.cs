using MECS.Variables.References;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Interface used on casting type data structures 
    public interface ICastingData
    {
        //Variables
        public EPhysicsCastingShape CastingShape { get; }
        public LayerMask IgnoreLayers { get; }
        public TransformReference[] OriginTransforms { get; }
        public TransformReference ScaleTransform { get; }
        public TransformReference DirectionTransform { get; }
        public QuaternionReference OrientationQuaternion { get; }
        public float MaxDistance { get; }
        public CastingStateMachine CastingStateMachine { get; }
        public bool IsCastingInitialized { get; set; }
        public Coroutine CastingExecution { get; set; }
    }
}