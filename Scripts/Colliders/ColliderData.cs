using System;
using MECS.Core;
using MECS.Entities.Enums;
using MECS.Events;
using MECS.Filters;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Colliders
{
    //* Data structure used on collider type entities
    [Serializable]
    public class ColliderData : AData, IColliderData, IFilterData, IEventData
    {
        //Editor variables
        #region COLLIDER_VALUES
        [Header("IColliderData values")]
        [SerializeField] private ColliderArrayReference colliderArrayReference = null;
        public ColliderArrayReference CollidersReference => colliderArrayReference;
        [SerializeField] private EColliderMode eColliderMode = EColliderMode.Collision;
        public EColliderMode EColliderMode => eColliderMode;
        [SerializeField] private EColliderCallback[] eColliderCallbacks = null;
        public EColliderCallback[] EColliderCallbacks => eColliderCallbacks;
        [SerializeField] private GameObject lastCollisionEntity = null;
        public GameObject LastCollisionEntity { get => lastCollisionEntity; set => lastCollisionEntity = value; }
        #endregion

        #region FILTER_VALUES
        [Header("IFilterData values")]
        [SerializeField] private ScriptableEnum[] scriptableEnums = null;
        public ScriptableEnum[] ScriptableEnums => scriptableEnums;
        [SerializeField] private IntArrayReference layersReference = null;
        public IntArrayReference LayersReference => layersReference;
        [SerializeField] private StringArrayReference tagsReference = null;
        public StringArrayReference TagsReference => tagsReference;
        [SerializeField] private GameObjectArrayReference entitiesReference = null;
        public GameObjectArrayReference EntitiesReference => entitiesReference;
        [SerializeField] private FilterCheckPassTrackingInfo[] filterCheckingPassesTrackingInformation = null;
        public FilterCheckPassTrackingInfo[] FilterCheckingPassesTrackingInformation
        { get => filterCheckingPassesTrackingInformation; set => filterCheckingPassesTrackingInformation = value; }
        #endregion

        #region EVENT_VALUES
        [Header("IEventData values")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;
        #endregion

        #region ADATA_LIFE_CYCLE_NOTIFICATION_METHODS
        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender)
        {
            //Args to send
            EntityAwakeArgs<IColliderData> colliderArgs = new((IColliderData)this, " couldnt notify collider data awake");
            EntityAwakeArgs<IFilterData> filterArgs = new((IFilterData)this, " couldnt notify filter data awake");
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify event data awake");

            //Notify IColliderData 
            NotifyDataPhase<EntityAwakeArgs<IColliderData>, IColliderData>(sender, colliderArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityAwakeArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender)
        {
            //Args to send
            EntityEnableArgs<IColliderData> colliderArgs = new((IColliderData)this, " couldnt notify collider data enable");
            EntityEnableArgs<IFilterData> filterArgs = new((IFilterData)this, " couldnt notify filter data enable");
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify event data enable");

            //Notify IColliderData 
            NotifyDataPhase<EntityEnableArgs<IColliderData>, IColliderData>(sender, colliderArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityEnableArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender)
        {
            //Args to send
            EntityDisableArgs<IColliderData> colliderArgs = new((IColliderData)this, " couldnt notify collider data disable");
            EntityDisableArgs<IFilterData> filterArgs = new((IFilterData)this, " couldnt notify filter data disable");
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify event data disable");

            //Notify IColliderData 
            NotifyDataPhase<EntityDisableArgs<IColliderData>, IColliderData>(sender, colliderArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityDisableArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender)
        {
            //Args to send
            EntityDestroyArgs<IColliderData> colliderArgs = new((IColliderData)this, " couldnt notify collider data destroy");
            EntityDestroyArgs<IFilterData> filterArgs = new((IFilterData)this, " couldnt notify filter data destroy");
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, " couldnt notify event data destroy");

            //Notify IColliderData 
            NotifyDataPhase<EntityDestroyArgs<IColliderData>, IColliderData>(sender, colliderArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityDestroyArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
        #endregion
    }
}