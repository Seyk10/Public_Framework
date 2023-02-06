using System;
using MECS.Core;
using MECS.Entities.Enums;
using MECS.Events;
using MECS.Filters;
using MECS.Tools;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Colliders
{
    //* Data structure used on collider type entities
    [Serializable]
    public class ColliderData : AData, IColliderData, IFilterData, IEventData
    {
        //Editor variables
        [Header("IColliderData values")]
        [SerializeField] private ColliderArrayReference colliderArrayReference = null;
        public ColliderArrayReference CollidersReference => colliderArrayReference;
        [SerializeField] private EColliderMode eColliderMode = EColliderMode.Collision;
        public EColliderMode EColliderMode => eColliderMode;
        [SerializeField] EColliderCallback[] eColliderCallbacks = null;
        public EColliderCallback[] EColliderCallbacks => eColliderCallbacks;

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

        [Header("IEventData values")]
        [SerializeField] private EventReference eventReference = null;
        public EventReference EventReference => eventReference;

        //AData method, notify data awake
        public override void NotifyDataAwake(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityAwakeArgs<IColliderData> colliderArgs = new((IColliderData)this, complexDebugInformation);
            EntityAwakeArgs<IFilterData> filterArgs = new((IFilterData)this, complexDebugInformation);
            EntityAwakeArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IColliderData 
            NotifyDataPhase<EntityAwakeArgs<IColliderData>, IColliderData>(sender, colliderArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityAwakeArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityAwakeArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data enable
        public override void NotifyDataEnable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityEnableArgs<IColliderData> colliderArgs = new((IColliderData)this, complexDebugInformation);
            EntityEnableArgs<IFilterData> filterArgs = new((IFilterData)this, complexDebugInformation);
            EntityEnableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IColliderData 
            NotifyDataPhase<EntityEnableArgs<IColliderData>, IColliderData>(sender, colliderArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityEnableArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityEnableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data disable
        public override void NotifyDataDisable(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDisableArgs<IColliderData> colliderArgs = new((IColliderData)this, complexDebugInformation);
            EntityDisableArgs<IFilterData> filterArgs = new((IFilterData)this, complexDebugInformation);
            EntityDisableArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IColliderData 
            NotifyDataPhase<EntityDisableArgs<IColliderData>, IColliderData>(sender, colliderArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityDisableArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDisableArgs<IEventData>, IEventData>(sender, eventArgs);
        }

        //AData method, notify data destroy
        public override void NotifyDataDestroy(MonoBehaviour sender, DebugTools.ComplexDebugInformation complexDebugInformation)
        {
            //Args to send
            EntityDestroyArgs<IColliderData> colliderArgs = new((IColliderData)this, complexDebugInformation);
            EntityDestroyArgs<IFilterData> filterArgs = new((IFilterData)this, complexDebugInformation);
            EntityDestroyArgs<IEventData> eventArgs = new((IEventData)this, complexDebugInformation);

            //Notify IColliderData 
            NotifyDataPhase<EntityDestroyArgs<IColliderData>, IColliderData>(sender, colliderArgs);
            //Notify IFilterData 
            NotifyDataPhase<EntityDestroyArgs<IFilterData>, IFilterData>(sender, filterArgs);
            //Notify IEventData
            NotifyDataPhase<EntityDestroyArgs<IEventData>, IEventData>(sender, eventArgs);
        }
    }
}