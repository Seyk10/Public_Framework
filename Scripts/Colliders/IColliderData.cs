using MECS.Variables.References;
using UnityEngine;

namespace MECS.Colliders
{
    //* Interface used on all collider data structures
    public interface IColliderData
    {
        //Variables
        public ColliderArrayReference CollidersReference { get; }
        public EColliderMode EColliderMode { get; }
        public EColliderCallback[] EColliderCallbacks { get; }
        public GameObject LastCollisionEntity { get; set; }
    }
}