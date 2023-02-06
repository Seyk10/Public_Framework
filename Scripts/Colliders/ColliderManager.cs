using System;
using MECS.Core;
using MECS.Variables.References;
using UnityEngine;

namespace MECS.Colliders
{
    //* Manager used to store all collider interfaces
    //* T = IColliderData
    [CreateAssetMenu(fileName = "New_Collider_Manager", menuName = "MECS/Managers/Collider")]
    public class ColliderManager : AManager<IColliderData>
    {
        //* Tracking information for collider data types
        [Serializable]
        private class ColliderTrackingInfo : ADataTrackingInfo, IColliderData
        {
            //Editor variables
            [Header("Collider tracking info")]
            [SerializeField] private ColliderArrayReference collidersReference = null;
            [SerializeField] private EColliderMode colliderMode = EColliderMode.Trigger;
            [SerializeField] private EColliderCallback[] colliderCallbacks = null;

            //Attributes
            public ColliderArrayReference CollidersReference => collidersReference;
            public EColliderMode EColliderMode => colliderMode;
            public EColliderCallback[] EColliderCallbacks => colliderCallbacks;

            //Default builder
            public ColliderTrackingInfo(string entityName, IColliderData colliderData) : base(entityName)
            {
                collidersReference = colliderData.CollidersReference;
                colliderMode = colliderData.EColliderMode;
                colliderCallbacks = colliderData.EColliderCallbacks;
            }
        }
    }
}