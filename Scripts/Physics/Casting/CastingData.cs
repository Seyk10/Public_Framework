using System;
using MECS.Core;
using MECS.Entities.Enums;
using MECS.Events;
using MECS.Filters;
using MECS.Tools;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Physics.Casting
{
    //* Data structure used on casting entities
    [Serializable]
    public class CastingData : AData, ICastingData, IFilterData, IEventData
    {
        #region CASTING_VALUES
        //ICastingData, editor variables
        [Header("Casting parameters")]
        [SerializeField] private EPhysicsCastingShape castingShape = EPhysicsCastingShape.Box;
        public EPhysicsCastingShape CastingShape => castingShape;
        [SerializeField] private LayerMask ignoreLayers = default;
        public LayerMask IgnoreLayers => ignoreLayers;
        [SerializeField] private TransformReference[] originTransforms = null;
        public TransformReference[] OriginTransforms => originTransforms;
        [SerializeField] private TransformReference scaleTransform = null;
        public TransformReference ScaleTransform => scaleTransform;
        [SerializeField] private TransformReference directionTransform = null;
        public TransformReference DirectionTransform => directionTransform;
        [SerializeField] private QuaternionReference orientationQuaternion = null;
        public QuaternionReference OrientationQuaternion => orientationQuaternion;
        [SerializeField] private float maxDistance = 0f;
        public float MaxDistance => maxDistance;

        //ICastingData, variables
        public CastingStateMachine castingStateMachine = new();
        public CastingStateMachine CastingStateMachine => castingStateMachine;
        public bool IsCastingInitialized { get; set; }
        public Coroutine CastingExecution { get; set; }
        #endregion

        #region FILTER_VALUES
        //IFilterData, editor variables
        [Header("Filter parameters")]
        [SerializeField] private IntArrayReference layersReference = null;
        public IntArrayReference LayersReference => layersReference;
        [SerializeField] private StringArrayReference tagsReference = null;
        public StringArrayReference TagsReference => tagsReference;
        [SerializeField] private ScriptableEnum[] scriptableEnums = null;
        public ScriptableEnum[] ScriptableEnums => scriptableEnums;
        [SerializeField] private GameObjectArrayReference entitiesReference = null;
        public GameObjectArrayReference EntitiesReference => entitiesReference;
        [SerializeField] private FilterCheckPassTrackingInfo[] filterCheckingPassesTrackingInformation = null;
        public FilterCheckPassTrackingInfo[] FilterCheckingPassesTrackingInformation
        { get => filterCheckingPassesTrackingInformation; set => filterCheckingPassesTrackingInformation = value; }
        #endregion

        #region EVENT_VALUES
        //IEventData, editor variables
        [Header("Event parameters")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;
        #endregion

        #region ADATA_LIFE_CYCLE_NOTIFICATION_METHODS
        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender)
        {
            //Args to send
            EntityAwakeArgs<ICastingData> castingArgs = new((ICastingData)this, " couldnt notify ICastingData awake");
            EntityAwakeArgs<IFilterData> filterArgs = new((IFilterData)this, " couldnt notify IFilterData awake");
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData awake");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityAwakeArgs<ICastingData>, ICastingData>(sender, castingArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityAwakeArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender)
        {
            //Args to send
            EntityEnableArgs<ICastingData> castingArgs = new((ICastingData)this, " couldnt notify ICastingData enable");
            EntityEnableArgs<IFilterData> filterArgs = new((IFilterData)this, " couldnt notify IFilterData enable");
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData enable");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityEnableArgs<ICastingData>, ICastingData>(sender, castingArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityEnableArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender)
        {
            //Args to send
            EntityDisableArgs<ICastingData> castingArgs = new((ICastingData)this, " couldnt notify ICastingData disable");
            EntityDisableArgs<IFilterData> filterArgs = new((IFilterData)this, " couldnt notify IFilterData disable");
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify IEventData disable");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityDisableArgs<ICastingData>, ICastingData>(sender, castingArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityDisableArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender)
        {
            //Args to send
            EntityDestroyArgs<ICastingData> castingArgs = new((ICastingData)this, " couldnt notify ICastingData destroy");
            EntityDestroyArgs<IFilterData> filterArgs = new((IFilterData)this, " couldnt notify ICastingData destroy");
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify ICastingData destroy");

            //Notify LifeCycleData 
            NotifyDataPhase<EntityDestroyArgs<ICastingData>, ICastingData>(sender, castingArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityDestroyArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
        #endregion
    }
}