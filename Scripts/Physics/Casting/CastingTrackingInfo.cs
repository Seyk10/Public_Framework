using MECS.Core;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Tracking information for ICastingData
    public class CastingTrackingInfo : ADataTrackingInfo, ICastingData
    {
        //Editor variables
        [Header("Casting tracking info")]
        [SerializeReference] private EPhysicsCastingShape castingShape = EPhysicsCastingShape.Box;
        [SerializeReference] private LayerMask ignoreLayers = default;
        [SerializeReference] private TransformReference[] originTransforms = null;
        [SerializeReference] private TransformReference scaleTransform = null;
        [SerializeReference] private TransformReference directionTransform = null;
        [SerializeReference] private QuaternionReference orientationQuaternion = null;
        [SerializeReference] private float maxDistance = 0f;
        [SerializeReference] private CastingStateMachine castingStateMachine = null;
        [SerializeReference] private bool isCastingInitialized = false;
        [SerializeReference] private Coroutine castingExecution = null;

        //Attributes
        public EPhysicsCastingShape CastingShape => castingShape;
        public LayerMask IgnoreLayers => ignoreLayers;
        public TransformReference[] OriginTransforms => originTransforms;
        public TransformReference ScaleTransform => scaleTransform;
        public TransformReference DirectionTransform => directionTransform;
        public QuaternionReference OrientationQuaternion => orientationQuaternion;
        public float MaxDistance => maxDistance;
        public CastingStateMachine CastingStateMachine => castingStateMachine;
        public bool IsCastingInitialized { get => isCastingInitialized; set => isCastingInitialized = value; }
        public Coroutine CastingExecution { get => castingExecution; set => castingExecution = value; }

        //Default builder
        public CastingTrackingInfo(string entityName, EPhysicsCastingShape castingShape, LayerMask ignoreLayers,
        TransformReference[] originTransforms, TransformReference scaleTransform, TransformReference directionTransform,
        QuaternionReference orientationQuaternion, float maxDistance, CastingStateMachine castingStateMachine,
        bool isCastingInitialized, Coroutine castingExecution) : base(entityName)
        {
            this.castingShape = castingShape;
            this.ignoreLayers = ignoreLayers;
            this.originTransforms = originTransforms;
            this.scaleTransform = scaleTransform;
            this.directionTransform = directionTransform;
            this.orientationQuaternion = orientationQuaternion;
            this.maxDistance = maxDistance;
            this.castingStateMachine = castingStateMachine;
            IsCastingInitialized = isCastingInitialized;
            CastingExecution = castingExecution;
            IsCastingInitialized = isCastingInitialized;
            CastingExecution = castingExecution;
        }
    }
}